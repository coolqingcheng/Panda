using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Panda.Web.Filters;

public class CSRFFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("执行前");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("执行后:" + context.HttpContext.Request.Path);

        var antiforgery = context.HttpContext.RequestServices.GetService<IAntiforgery>();
        var tokens = antiforgery?.GetAndStoreTokens(context.HttpContext);
        context.HttpContext.Response.Cookies.Append("CSRF-TOKEN", tokens.RequestToken,
            new Microsoft.AspNetCore.Http.CookieOptions {HttpOnly = false});
    }
}