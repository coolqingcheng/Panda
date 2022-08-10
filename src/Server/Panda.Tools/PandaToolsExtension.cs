using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Auth.Permission;
using Panda.Tools.Email;
using Panda.Tools.FileStorage;
using Panda.Tools.Helper;

namespace Panda.Tools;

public static class PandaToolsExtension
{
    public static IServiceCollection AddTools(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IEmailSender, DefaultEmailSender>();
        serviceCollection.AddScoped<IFileStorage, LocalFileStorage>();
        serviceCollection.AddSingleton<IpHelper>();
        serviceCollection.AddPermission();
        return serviceCollection;
    }

    public static void UseTools(this IApplicationBuilder app)
    {
        app.UsePermission();
    }
}