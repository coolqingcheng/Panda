using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Panda.Web.Areas.Admin;


public class AccountController : AdminBaseController
{
    // GET
    public async Task<IActionResult> Index()
    {
        var identity = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name,"qingcheng")
        },CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(claimsPrincipal);
        //后台全部走ajax请求，请求头header必须带上 X-Requested-With=XMLHttpRequest，否则中间件会执行重定向
        return Content("admin");
    }

    [HttpGet]
    public async Task LoginOutAsync()
    {
        await HttpContext.SignOutAsync();
    }

    [Authorize]
    [HttpGet]
    public  IActionResult AuthTest()
    {
        HttpContext.Request.Query.
        return Content("登录成功");
    }
}