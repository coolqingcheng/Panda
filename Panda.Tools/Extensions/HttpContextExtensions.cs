using Microsoft.AspNetCore.Http;

namespace Panda.Tools.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    ///     获取当前登录人Id
    /// </summary>
    /// <param name="http"></param>
    /// <returns></returns>
    public static Guid? CurrentAccountId(this HttpContext http)
    {
        var identity = http.User;
        var id = identity.Claims.Where(a => a.Type == "Id")
            .Select(a => a.Value).FirstOrDefault();
        return id != null ? Guid.Parse(id) : Guid.Empty;
    }

    /// <summary>
    ///     获取客户端的真实IP地址
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public static string GetClientIP(this IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext!.Request;
        if (request.Headers.ContainsKey("X-Real-IP")) return request.Headers["X-Real-IP"].ToString();

        if (request.Headers.ContainsKey("X-Forwarded-For")) return request.Headers["X-Forwarded-For"].ToString();

        return "";
    }

    /// <summary>
    ///     是否是Ajax请求
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    public static bool IsAjax(this HttpRequest req)
    {
        var result = false;

        var xreq = req.Headers.ContainsKey("x-requested-with");
        if (xreq) result = req.Headers["x-requested-with"] == "XMLHttpRequest";

        return result;
    }
}