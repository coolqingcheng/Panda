using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Panda.Tools.CSRF;

public class CSRFFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var antiforgery = context.HttpContext.RequestServices.GetService<IAntiforgery>();
        if (context.HttpContext.Request.Method.ToLower() == "get")
        {
            var tokens = antiforgery?.GetAndStoreTokens(context.HttpContext);
            if (tokens?.RequestToken != null)
                context.HttpContext.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken,
                    new CookieOptions { HttpOnly = false });
        }
    }
}