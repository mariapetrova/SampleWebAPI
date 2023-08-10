using Microsoft.Extensions.Configuration;

namespace Functions.StartupExtensions;
public static class ConfigBuilder
{
    public static void BuildConfig(
        this IConfigurationBuilder builder)
    {
        var environemnt = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") ?? "Development.Local";

        var root = builder
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.{environemnt}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
        .Build();
    }
}