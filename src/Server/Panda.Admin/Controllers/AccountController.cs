using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Admin.Models;
using Panda.Admin.Models.Accounts;
using Panda.Admin.Models.Request;
using Panda.Admin.Services.Account;
using Panda.Entity.Responses;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Request;
using Panda.Tools.Auth.Response;
using Panda.Tools.Exception;
using Panda.Tools.Web;

namespace Panda.Admin.Controllers;

[PermissionGroup("用户管理")]
public class AccountController : AdminController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    ///     登录
    /// </summary>
    /// <param name="request"></param>
    /// <exception cref="UserException"></exception>
    [AllowAnonymous]
    [HttpPost]
    [Permission("登录")]
    public async Task Login(AccountLoginRequest request)
    {
        var res = await _accountService.LoginAsync(request.UserName, request.Password);
        if (res.IsSuccess == false) throw new UserException(res.Message);
    }

    [HttpGet]
    public async Task LoginOutAsync()
    {
        await HttpContext.SignOutAsync();
    }

    [AllowAnonymous]
    [HttpPost("/admin/createAdminAccount")]
    public async Task<IActionResult> CreateAdminAccount(CreateAdminAccountRequest request)
    {
        await _accountService.CreateAdminAccount(request);
        return Content("初始化账号成功");
    }


    /// <summary>
    /// 检查当前状态
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<LoginStatusResult> IsLogin()
    {
        var isLogin = HttpContext.User.Identity is { IsAuthenticated: true };
        return new LoginStatusResult()
        {
            IsLogin = isLogin,
            IsInit = await _accountService.CheckAdminAccountExistAsync()
        };
    }

    [HttpPost]
    [Permission("修改密码")]
    public async Task ChangePwd(ChangePwdRequest request)
    {
        await _accountService.ChangePwdAsync(request);
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [HttpGet]
    [Permission("用户列表")]
    public async Task<PageDto<AccountResp>> GetAccountList([FromQuery] AccountReq req)
    {
        return await _accountService.GetAccountList(req);
    }

    /// <summary>
    /// 添加用户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task AddOrUpdate(CreateAccountModel model)
    {
        if (model.Id.HasValue)
        {
            await _accountService.EditAccount(model);
        }
        else
        {
            await _accountService.CreateAccount(model);
        }
    }
}