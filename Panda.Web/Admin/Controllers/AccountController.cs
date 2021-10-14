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

    [Authorize]
    [HttpGet]
    public IActionResult AuthTest()
    {
        return Content("登录成功");
    }
}