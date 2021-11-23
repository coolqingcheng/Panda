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
            var identity = http.User.Identities.Where(a => a.Name == "Id").Where(a => a.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme).SelectMany(a => a.Claims).ToList();
            var claim = identity.Where(a => a.Type == "Id").FirstOrDefault();
            if (claim != null)
            {
                return Guid.Parse(claim.Value);
            }
            return Guid.Empty;
        }
    }
}
