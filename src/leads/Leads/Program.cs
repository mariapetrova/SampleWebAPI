using Functions.Common;
using Functions.StartupExtensions;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using Infrastucture;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, builder) =>
    {
        builder.BuildConfig();
    })
    .ConfigureFunctionsWorkerDefaults(worker =>
    {
        var serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
        };
        serializerSettings.Converters.Add(new StringEnumConverter());

        worker.UseNewtonsoftJson(serializerSettings);

        worker.UseFunctionExceptionMiddleware();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddFunctionContextAccessor();
        services.AddApplicationServices(Assembly.LoadFrom(AssemblyConstants.PersonsAssemblyName));
        services.AddInfrastructureServices(context.Configuration);

        services.AddOpenApiConfigurationOptions();
    })
    .ConfigureOpenApi()
    .UseSerilog((tbc, lc) =>
    {
        lc.ReadFrom.Configuration(tbc.Configuration)
        .WriteTo.ApplicationInsights(
            tbc.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey"),
            TelemetryConverter.Traces);
    })
    .Build();

host.Run();