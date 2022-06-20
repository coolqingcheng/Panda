using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Panda.Tools.Web;

public class App
{
    private static HttpContext? Context => HttpContextLocal.GetCurrentHttpContext();

    public static TService? GetService<TService>() where TService : class
    {
        return Context?.RequestServices.GetService<TService>();
    }
}