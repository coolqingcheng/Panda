using System.Security.Claims;
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

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
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
        var res =  HttpContext.User.Identity is { IsAuthenticated: true };
        if (res) return true;
        throw new UserException("登录信息失效，请重新登录");
    }
}