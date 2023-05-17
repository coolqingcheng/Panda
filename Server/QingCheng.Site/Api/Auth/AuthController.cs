using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QingCheng.Site.Api.Auth.Models;
using QingCheng.Site.Api.Services;
using QingCheng.Tools.Auth;
using QingCheng.Tools.Controllers;
using QingCheng.Site.Models.Auth;
using QingCheng.Site.Services.Sys;
using QingCheng.WebApi.Services;

namespace QingCheng.Site.Api.Auth;

/// <summary>
/// 授权认证
/// </summary>
public class AuthController : BaseAdminController
{
    private readonly AccountService _account;

    private readonly CookieAuthHelper _cookieAuth;


    public AuthController(AccountService account, CookieAuthHelper cookieAuth)
    {
        _account = account;
        _cookieAuth = cookieAuth;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    [HttpPost, AllowAnonymous]
    public async Task Login(LoginModel model)
    {
        var res = await _account.LoginAsync(model.UserName, model.Pwd);
        if (res.IsOk == false)
        {
           
            throw new UserException($"登录失败！{res.Message}");
        }

        await _cookieAuth.CookieLogin(new List<Claim>()
        {
            new("Id", res.Data.Id.ToString()),
            new("Name",res.Data.UserName)
        });
    }
    /// <summary>
    /// 退出
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task LoginOut()
    {
        await _cookieAuth.CookieSignOutAsync();
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task ChangeCurrPwd(ChangeCurrPwd model)
    {
        await _account.ChangePassword(HttpContext.CurrUserId(), model.OldPwd, model.NewPwd);
        await _cookieAuth.CookieSignOutAsync();
    }
}