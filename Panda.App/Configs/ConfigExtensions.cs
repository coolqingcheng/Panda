using Panda.Entity.Options;

namespace Panda.App.Configs;

public static class ConfigExtensions
{
    public static void AddConfig(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.Configure<SiteOption>(
            configuration.GetSection(SiteOption.Key)
        );
    }
}