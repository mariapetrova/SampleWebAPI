using Functions.Common;
using Functions.StartupExtensions;
using Infrastucture;
using Infrastucture.Persistence;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Leads.Application.IntegrationTests.Fixtures;
public sealed class HostBuilderFixture : IDisposable
{
    private readonly IHost _host;

    private readonly IServiceScopeFactory _scopeFactory = null!;

    public HostBuilderFixture()
    {
        _host = new HostBuilder()
        .ConfigureAppConfiguration((_, builder) =>
        {
            builder
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();
        })
        .ConfigureServices((context, services) =>
        {
            var assemblyLocation = Assembly.GetEntryAssembly().Location;
            services.AddApplicationServices(GetInvokingAssemblyName(assemblyLocation));
            services.AddInfrastructureServices(context.Configuration);
        })
        .Build();

        _scopeFactory = _host.Services.GetRequiredService<IServiceScopeFactory>();
    }


    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        entity = context.Add(entity).Entity;

        await context.SaveChangesAsync();

        return entity;
    }

    public async Task AddRangeAsync<TEntity>(List<TEntity> entities)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.AddRange(entities);

        await context.SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync<TEntity>(
       ExpressionStarter<TEntity> filter,
       CancellationToken cancellationToken)
       where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var dbSet = context.Set<TEntity>();

        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public void Dispose()
    {
        _host?.Dispose();
    }

    private static Assembly GetInvokingAssemblyName(string location)
    {
        const string suffixToTrim = ".IntegrationTests";
        var invokingAssemblyName = string.Empty;

        var splittedLocation = location.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        var assemblyNames = typeof(AssemblyConstants).GetFields().Select(x => x.GetRawConstantValue().ToString()).ToList();

        var assembly = splittedLocation.Where(x => x.EndsWith(suffixToTrim, StringComparison.OrdinalIgnoreCase))
                                       .FirstOrDefault()
                                       .Replace(suffixToTrim, string.Empty, StringComparison.OrdinalIgnoreCase)
                                       .TrimEnd();
        invokingAssemblyName = assemblyNames.FirstOrDefault(x => x.Contains(assembly, StringComparison.OrdinalIgnoreCase));

        return Assembly.LoadFrom(invokingAssemblyName);
    }
}

