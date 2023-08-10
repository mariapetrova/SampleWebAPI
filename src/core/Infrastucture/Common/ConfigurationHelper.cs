using Microsoft.Extensions.Configuration;

namespace Infrastucture.Common;
public static class ConfigurationHelper
{
    public static IConfiguration Config { get; set; }

    public static void Initialize(IConfiguration configuration)
    {
        Config = configuration;
    }
}
