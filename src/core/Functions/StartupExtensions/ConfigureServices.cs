using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Functions.StartupExtensions;
public static class ConfigureServices
{
    /// <summary>
    /// Adds a default implementation for the <see cref="IFunctionContextAccessor"/> service.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddFunctionContextAccessor(
        this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    /// Adds a default implementation for the <see cref="IOpenApiConfigurationOptions"/> service.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    public static void AddOpenApiConfigurationOptions(
        this IServiceCollection services)
    {
        services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions()
            {
                Info = new OpenApiInfo()
                {
                    Version = "1.0.0",
                    Title = GetTitle(),
                    Description = "This is a sample server API designed by [http://swagger.io](http://swagger.io).",
                    TermsOfService = new Uri("https://github.com/Azure/azure-functions-openapi-extension"),
                },
                Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                OpenApiVersion = OpenApiVersionType.V3,
                IncludeRequestingHostName = true,
                ForceHttps = false,
                ForceHttp = false,
            };

            return options;
        })
        .AddSingleton<IOpenApiHttpTriggerAuthorization>(_ =>
        {
            var auth = new OpenApiHttpTriggerAuthorization(req =>
            {
                var result = default(OpenApiAuthorizationResult);

                // ⬇️⬇️⬇️ Add your custom authorization logic ⬇️⬇️⬇️
                //
                // CUSTOM AUTHORISATION LOGIC
                //
                // ⬆️⬆️⬆️ Add your custom authorization logic ⬆️⬆️⬆️
                return Task.FromResult(result);
            });

            return auth;
        });
    }

    private static string GetTitle()
    {
        var title = Assembly.GetEntryAssembly().GetName().Name
            .Split(".", StringSplitOptions.RemoveEmptyEntries).Last();

        return title;
    }
}