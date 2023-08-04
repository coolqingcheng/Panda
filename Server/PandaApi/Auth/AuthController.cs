﻿using System.Security.Claims;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Models.Dtos.Account;
using Panda.Models.Dtos.Auth;
using Panda.Services.Sys;
using PandaTools.Auth;
using PandaTools.Exceptions;

namespace PandaApi.Auth;

/// <summary>
///     授权认证
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
    ///     登录
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task Login(LoginModel model, [FromServices] ICaptcha captcha)
    {
        //检验验证码
        if (HttpContext.Request.Cookies.TryGetValue("captcha-id", out var guid))
        {
            var captchaId = $"{(int)ValidateCodeType.Login}_{guid}";
            if (captcha.Validate(captchaId, model.ValidCode, removeIfSuccess: true) == false)
            {
                throw new UserException("验证码错误");
            }
        }
        else
        {
            throw new UserException("验证码错误");
        }

        var res = await _account.LoginAsync(model.UserName, model.Pwd);
        if (res.IsOk == false) throw new UserException($"登录失败！{res.Message}");

        await _cookieAuth.CookieLogin(new List<Claim>
        {
            new("Id", res.Data.Id.ToString()),
            new("Name", res.Data.UserName)
        });
    }

    /// <summary>
    ///     退出
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task LoginOut()
    {
        await _cookieAuth.CookieSignOutAsync();
    }

    /// <summary>
    ///     修改密码
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