using Microsoft.AspNetCore.Http;

namespace PandaTools.Helper
{
    public static class HttpExtensions
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isProxy"></param>
        /// <returns></returns>
        public static string GetClientIP(this HttpContext context, bool isProxy = false)
        {
            string? ip;
            if (isProxy)
            {
                ip = context.Request.Headers.Where(a => a.Key == "Cdn-Src-Ip").Select(a => a.Value.ToString()).FirstOrDefault();
                if (!string.IsNullOrEmpty(ip))
                    return IpReplace(ip);

                ip = context.Request.Headers.Where(a => a.Key == "X-Forwarded-For").Select(a => a.Value.ToString()).FirstOrDefault();
                if (!string.IsNullOrEmpty(ip))
                    return IpReplace(ip);
            }

            ip = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrWhiteSpace(ip))
            {
                return "未知";
            }
            return IpReplace(ip);
        }

        static string IpReplace(string inip)
        {
            //::ffff:
            //::ffff:192.168.2.131 这种IP处理
            if (inip.Contains("::ffff:"))
            {
                inip = inip.Replace("::ffff:", "");
            }
            return inip;
        }
    }
}
