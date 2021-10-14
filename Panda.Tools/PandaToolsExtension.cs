using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Security;

namespace Panda.Tools;

public static class PandaToolsExtension
{
    public static IServiceCollection AddTools(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IdentitySecurity>();
        return serviceCollection;
    }
}