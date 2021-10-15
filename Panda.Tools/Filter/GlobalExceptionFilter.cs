using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Panda.Tools.Exception;

namespace Panda.Tools.Filter;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is UserException exception)
        {
            context.Result = new JsonResult(new {Message = exception.Message});
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }
    }
}