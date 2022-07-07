using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Panda.Tools.Exception;
using System.Net;
using System.Security.Claims;

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
    /// <summary>
    /// 检查当前是否是管理员身份
    /// </summary>
    /// <returns></returns>
    public static bool IsAdmin()
    {
        var claimsIdentity = Context?.User.Identity as ClaimsIdentity;
        var isAdmin = claimsIdentity?.Claims.Where(a => a.Type == "IsAdmin").Select(a => a.Value.ToLower() == "true").First();
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
}