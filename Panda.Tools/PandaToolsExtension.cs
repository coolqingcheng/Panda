using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.FileStorage;
using Panda.Tools.Security;

namespace Panda.Tools;

public static class PandaToolsExtension
{
    public static IServiceCollection AddTools(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IdentitySecurity>();
        serviceCollection.AddScoped<IFileStorage, LocalFileStorage>();
        return serviceCollection;
    }
}