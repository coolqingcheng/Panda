using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaTools.MiddleWare
{
    public class SiteUIdMiddleWare
    {
        public const string SiteUidCookeKey = "site-uid";
        private readonly RequestDelegate _next;
        public SiteUIdMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.SetSiteUid();
            await _next(context);
        }
    }


    public static class SiteUidExtensions
    {

        public static void UseSiteUid(this IApplicationBuilder app)
        {
            app.UseMiddleware<SiteUIdMiddleWare>();
        }

        public static string SetSiteUid(this HttpContext context)
        {
            var UId = context.Request.Cookies.FirstOrDefault(a => a.Key == SiteUIdMiddleWare.SiteUidCookeKey).Value;
            if (string.IsNullOrWhiteSpace(UId))
            {
                UId = Guid.NewGuid().ToString("N");
                context.Response.Cookies.Delete(SiteUIdMiddleWare.SiteUidCookeKey);
                context.Response.Cookies.Append(SiteUIdMiddleWare.SiteUidCookeKey, UId, new CookieOptions() { Expires = DateTimeOffset.MaxValue });

            }
            return UId;
        }

        public static string GetSiteUid(this HttpContext context)
        {
            var uid = context.Request.Cookies.FirstOrDefault(a => a.Key == SiteUIdMiddleWare.SiteUidCookeKey).Value;
            if (string.IsNullOrWhiteSpace(uid))
            {
                return context.SetSiteUid();
            }
            return uid;
        }
    }
}
