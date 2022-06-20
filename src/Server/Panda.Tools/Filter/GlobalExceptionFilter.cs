using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;

namespace Panda.Tools.Filter;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is UserException exception)
        {
            if (context.HttpContext.Request.IsAjax())
            {
                context.Result = new JsonResult(new {exception.Message});
                context.ExceptionHandled = true;
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            else
            {
                context.HttpContext.Items["ErrorMsg"] = exception.Message;
                context.ExceptionHandled = true;
                context.HttpContext.Response.Redirect("/404.html");
            }
        }
    }
}