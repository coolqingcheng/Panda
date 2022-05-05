using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Email;
using Panda.Tools.FileStorage;

namespace Panda.Tools;

public static class PandaToolsExtension
{
    public static IServiceCollection AddTools(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDistributedMemoryCache();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IEmailSender, DefaultEmailSender>();
        serviceCollection.AddScoped<IFileStorage, LocalFileStorage>();
        return serviceCollection;
    }

    public static void AddAopCache(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureDynamicProxy();
        builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
    }
}