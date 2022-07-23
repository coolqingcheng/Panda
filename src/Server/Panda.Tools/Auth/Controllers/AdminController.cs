using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        var logger = context.HttpContext.RequestServices.GetService<ILogger<AdminController>>();
        var host = $"{request.Scheme}://{request.Host.Value}";
        var referer = context.HttpContext.Request.Headers.Referer.ToString();
        logger?.LogInformation($" 验证 host和referer: {host}  {referer}");
        if (referer.StartsWith(host))
        {
            await next();
        }
        else
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                message = "访问失败！"
            });
        }
    }
}