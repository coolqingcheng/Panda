using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Email;
using Panda.Tools.FileStorage;
using Panda.Tools.Helper;

namespace Panda.Tools;

public static class PandaToolsExtension
{
    public static IServiceCollection AddTools(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDistributedMemoryCache();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IEmailSender, DefaultEmailSender>();
        serviceCollection.AddScoped<IFileStorage, LocalFileStorage>();
        serviceCollection.AddSingleton<IpHelper>();
        return serviceCollection;
    }
}