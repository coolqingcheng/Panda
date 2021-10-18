using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Services.Account;
using Panda.Tools.Exception;
using Panda.Web.Admin.Models;

namespace Panda.Web.Admin.Controllers;

public class AccountController : AdminBaseController
{
    private readonly IAccountService _accountService;

    private readonly IAntiforgery _antiforgery;

    public AccountController(IAccountService accountService, IAntiforgery antiforgery)
    {
        _accountService = accountService;
        _antiforgery = antiforgery;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task Login(AccountLoginRequest request)
    {
        var res = await _accountService.LoginAsync(request.UserName, request.Password);
        if (res.IsSuccess == false)
        {
            throw new UserException(res.Message);
        }

        var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
        HttpContext.Response.Cookies.Append("X-CSRF-TOKEN", tokens.CookieToken, new CookieOptions { HttpOnly = false });
        HttpContext.Response.Cookies.Append("X-CSRF-FORM-TOKEN", tokens.RequestToken,
            new CookieOptions { HttpOnly = false });
    }

    [HttpGet]
    public async Task LoginOutAsync()
    {
        await HttpContext.SignOutAsync();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Test()
    {
        await _accountService.InitAsync();
        return Content("初始化账号成功");
    }


    [HttpGet]
    public bool IsLogin()
    {
        var res = HttpContext.User.Identity is { IsAuthenticated: true };
        if (res) return true;
        throw new UserException("登录信息失效，请重新登录");
    }
}