using Microsoft.Extensions.DependencyInjection;
using QingCheng.Site.Services;
using QingCheng.Tools;

namespace QingCheng.Site;

public static class SiteModule
{
    public static void AddRazorSite(this IServiceCollection services)
    {

        services.InjectServices(typeof(Program));
    }
}