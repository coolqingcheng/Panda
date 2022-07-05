using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Exception;
using System.Security.Claims;

namespace Panda.Tools.Web;

public class App
{
    private static HttpContext? Context => HttpContextLocal.GetCurrentHttpContext();

    public static TService? GetService<TService>() where TService : class
    {
        return Context?.RequestServices.GetService<TService>();
    }

    public static Guid GetAccountId()
    {
        var claimsIdentity = Context?.User.Identity as ClaimsIdentity;
        var accountId = claimsIdentity?.Claims.Where(a => a.Type == "id").Select(a => a.Value).FirstOrDefault();
        if (string.IsNullOrWhiteSpace(accountId))
        {
            throw new UserException("获取用户信息失败");
        }
        return Guid.Parse(accountId);
    }
}