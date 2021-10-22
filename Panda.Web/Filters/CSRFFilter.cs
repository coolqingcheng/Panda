using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Panda.Web.Filters;

public class CSRFFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var antiforgery = context.HttpContext.RequestServices.GetService<IAntiforgery>();
        var tokens = antiforgery?.GetAndStoreTokens(context.HttpContext);
        if (tokens?.RequestToken != null)
            context.HttpContext.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken,
                new Microsoft.AspNetCore.Http.CookieOptions { HttpOnly = false });
    }
}