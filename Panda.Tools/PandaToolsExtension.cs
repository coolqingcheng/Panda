using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Email;
using Panda.Tools.FileStorage;
using Panda.Tools.Security;
using Panda.Tools.Web;

namespace Panda.Tools;

public static class PandaToolsExtension
{
    public static IServiceCollection AddTools(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IEmailSender, DefaultEmailSender>();
        serviceCollection.AddScoped<IFileStorage, LocalFileStorage>();
        return serviceCollection;
    }
}