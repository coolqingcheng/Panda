using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Email;
using Panda.Tools.FileStorage;

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