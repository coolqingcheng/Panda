using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Exception;
using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;

namespace Panda.Tools.Web;

public class App
{
    private static HttpContext? Context => HttpContextLocal.GetCurrentHttpContext();

    public static TService? GetService<TService>() where TService : class
    {
        return Context?.RequestServices.GetService<TService>();
    }

    /// <summary>
    /// 获取当前的AccountId 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UserException"></exception>
    public static Guid GetAccountId()
    {
        var claimsIdentity = Context?.User.Identity as ClaimsIdentity;
        var accountId = claimsIdentity?.Claims.Where(a => a.Type == "Id").Select(a => a.Value).FirstOrDefault();
        if (string.IsNullOrWhiteSpace(accountId))
        {
            throw new UserException("获取用户信息失败", HttpStatusCode.Unauthorized);
        }

        return Guid.Parse(accountId);
    }

    public static async Task<Accounts> GetAccount()
    {
        var db = GetService<DbContext>();
        return await db.Set<Accounts>().Where(a => a.Id == GetAccountId()).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 检查当前是否是管理员身份
    /// </summary>
    /// <returns></returns>
    public static bool IsAdmin()
    {
        var claimsIdentity = Context?.User.Identity as ClaimsIdentity;
        var isAdmin = claimsIdentity?.Claims.Where(a => a.Type == "IsAdmin").Select(a => a.Value.ToLower() == "true")
            .First();
        return isAdmin!.Value;
    }

    /// <summary>
    /// 获取验证器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UserException"></exception>
    public static IValidator<T> Validator<T>() where T : class
    {
        var validator = GetService<IValidator<T>>();
        if (validator == null)
        {
            throw new UserException($"{typeof(T).Name}没有对应的验证器");
        }

        return validator;
    }

    /// <summary>
    ///  写入cookie到浏览器
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetCookie(string key, string value)
    {
        Context?.Response.Cookies.Append(key, value, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddYears(10)
        });
    }

    public static string? GetCookie(string key)
    {
        return Context?.Request.Cookies.TryGetValue(key, out var value) == true ? value : null;
    }

    public static void DeleteCookie(string key)
    {
        if (Context?.Request.Cookies.TryGetValue(key, out var _) == true)
        {
            Context?.Response.Cookies.Delete(key);
        }
    }
}