using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Services.Account;

namespace Panda.Web.Areas.Admin;

public class AccountController : AdminBaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // GET
    public async Task<IActionResult> Login()
    {
        
        return Content("admin");
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