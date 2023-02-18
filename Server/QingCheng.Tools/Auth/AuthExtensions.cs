using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Tools.Auth
{
    public static class AuthExtensions
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Guid CurrUserId(this HttpContext context)
        {
            var id = context.User.Claims.Where(a => a.Type == "Id").Select(a => a.Value).FirstOrDefault();
            return Guid.Parse(id!);
        }
    }
}
