using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Panda.Tools.Auth.Controllers;

/// <summary>
///     后台api
/// </summary>
[Route("/admin/[controller]/[action]")]
[Authorize]
[ApiController]
public class AdminController : Controller
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var request = context.HttpContext.Request;
        var host = $"{request.Scheme}://{request.Host.Value}";
        var referer = context.HttpContext.Request.Headers.Referer.ToString();
        if (referer.StartsWith(host))
        {
            await next();
        }
        else
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                message = "访问失败！"
            });
        }
    }
}