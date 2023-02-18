using Microsoft.Extensions.DependencyInjection;
using QingCheng.Site.Services;
using QingCheng.Tools;

namespace QingCheng.Site;

public static class SiteModule
{
    public static void AddSite(this IServiceCollection services)
    {

        services.AddRazorPages();
        services.InjectServices(typeof(Program));
    }
}