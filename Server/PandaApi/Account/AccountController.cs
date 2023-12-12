using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Models.Dtos.Account;
using Panda.Services.Account;
using Panda.Services.Sys;
using PandaTools.Auth;

namespace PandaApi.Account;

/// <summary>
///     账号管理
/// </summary>
public class AccountController : BaseAdminController
{
    private readonly AccountService _service;

    private DbContext _context;

    public AccountController(AccountService service, DbContext context)
    {
        _service = service;
        _context = context;
    }

    /// <summary>
    ///     初始化系统账号
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> InitAccount()
    {
        var res = await _context.Database.EnsureCreatedAsync();

        return Content("初始化结果:" + res);
    }


    /// <summary>
    ///     账户列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageDto<AccountItemDto>> GetList([FromQuery] AccountListRequest request)
    {
        return await _service.GetList(request);
    }

    /// <summary>
    ///     禁止登录
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task AccountForbidLogin(Guid accountId)
    {
        await _service.ForbidLogin(accountId);
    }

    /// <summary>
    ///     创建用户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task CreateUser(CreateUserModel model)
    {
        await _service.AddAccount(model.UserName, model.Password, model.Email ?? "");
    }

    /// <summary>
    ///     修改密码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task UpdatePwd(UpdatePwdModel model)
    {
        await _service.ChangePassword(model.id, model.NewPwd);
    }

    /// <summary>
    /// 获取最近登录记录
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public Task<PageDto<AccountLoginRecordDto>> GetLoginRecord([FromQuery] BasePageModel model)
    {
        var userId = HttpContext.CurrUserId();
        return _service.GetLoginRecord(model, userId);
    }
}