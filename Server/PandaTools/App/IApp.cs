using Microsoft.AspNetCore.Http;
using PandaTools.Helper;

namespace PandaTools.App;

public interface IApp
{
    string GetClientIp();

    string GetUserAgent();
}

public class WebApp : IApp
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WebApp(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetClientIp()
    {
        var ip = _httpContextAccessor.HttpContext?.GetClientIp();
        return ip ?? "未知";
    }

    public string GetUserAgent()
    {
        var userAgent = _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString();
        return string.IsNullOrWhiteSpace(userAgent) ? "" : userAgent;
    }
}