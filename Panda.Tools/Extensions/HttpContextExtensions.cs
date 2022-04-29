using System.Text;
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
       
        var ip = httpContextAccessor.HttpContext!.Request.Headers["X-Forwarded-For"].FirstOrDefault(); // 解决 nginx、docker等 获取ip问题
        if (string.IsNullOrEmpty(ip)) ip =httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        return "/";
    }
    
    /// <summary>
    /// 获取当前请求完整的Url地址
    /// </summary>
    /// <returns></returns>
    public static string GetCompleteUrl( this IHttpContextAccessor httpContextAccessor)
    { 
        return new StringBuilder()
            .Append(httpContextAccessor.HttpContext?.Request.Scheme)
            .Append("://")
            .Append(httpContextAccessor.HttpContext.Request.Host)
            .Append(httpContextAccessor.HttpContext.Request.PathBase)
            .Append(httpContextAccessor.HttpContext.Request.Path)
            .Append(httpContextAccessor.HttpContext.Request.QueryString)
            .ToString();
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