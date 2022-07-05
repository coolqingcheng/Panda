using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Panda.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Tools.Exception
{
    public static class ExceptionExtensions
    {
        public static void UseMyEx(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public ExceptionMiddleware(RequestDelegate request)
        {
            _requestDelegate = request;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate.Invoke(context);
            }
            catch (UserException ex)
            {
                await ResponseError(context, ex);
            }
            catch (System.Exception ex)
            {
                await ResponseError(context, ex);
            }
        }

        private async Task ResponseError(HttpContext context, System.Exception exp)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (context.Request.IsResponseJson())
            {
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = exp.Message
                });
            }
            else
            {
                context.Items["exp"] = exp;
                context.Response.Redirect("/500.html");
            }
        }
    }
}
