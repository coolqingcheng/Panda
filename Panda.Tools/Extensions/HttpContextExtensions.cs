using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Tools.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取当前登录人Id
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        public static Guid? CurrentAccountId(this HttpContext http)
        {
            var identity = http.User;
            var id = identity.Claims.Where(a => a.Type == "Id")
                .Select(a => a.Value).FirstOrDefault();
            return id != null ? Guid.Parse(id) : Guid.Empty;
        }

        /// <summary>
        /// 获取客户端的真实IP地址
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static string GetClientIP(this IHttpContextAccessor httpContextAccessor)
        {
            var request = httpContextAccessor.HttpContext!.Request;
            if (request.Headers.ContainsKey("X-Real-IP"))
            {
                return request.Headers["X-Real-IP"].ToString();
            }

            if (request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return request.Headers["X-Forwarded-For"].ToString();
            }

            return "";
        }
    }
}