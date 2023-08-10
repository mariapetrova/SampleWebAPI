using Application.Common.Interfaces;
using Infrastructure.Options;
using Infrastructure.Persistence.Services;
using Infrastucture.Common;
using Infrastucture.Persistence;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastucture;
public static class ConfigureServices
{
    /// <summary>
    /// Registers services required for the infrastructure.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<StorageBlobSettingsOptions>(configuration.GetSection(StorageBlobSettingsOptions.StorageBlobSettings));
        ConfigurationHelper.Initialize(configuration);

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("SampleWebAPI.SampleWebAPIDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetSection("DefaultConnection").Value,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        if (configuration.GetSection("AzureStorageBlob") != null)
        {
            services.AddStorageService(configuration);
        }

        return services;
    }

    private static void AddStorageService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(options =>
            options.AddBlobServiceClient(configuration.GetSection("AzureStorageBlob:DefaultConnection").Value));

        services.AddScoped<IStorageService, StorageService>();
    }
}
