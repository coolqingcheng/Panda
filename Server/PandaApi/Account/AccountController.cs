using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Models.Dtos.Account;
using Panda.Services.Sys;
using PandaApi.Events;

namespace PandaApi.Account;

/// <summary>
/// 账号管理
/// </summary>
public class AccountController : BaseAdminController
{
    private readonly IMediator _mediator;

    private readonly AccountService _service;

    public AccountController(IMediator mediator, AccountService service)
    {
        _mediator = mediator;
        _service = service;
    }

    /// <summary>
    /// 初始化系统账号
    /// </summary>
    /// <returns></returns>
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> InitAccount()
    {
        var res = await _service.AddAccount("admin", "admin", "admin@qq.com");

        return new JsonResult(res);
    }

    /// <summary>
    /// 测试event
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<string?> Test([FromServices]IHttpContextAccessor httpContext)
    {
        var res = await _mediator.Send(new TestModel()
        {
            Time = DateTime.Now
        });
        Console.WriteLine("结果:" + res);
        return httpContext.HttpContext?.TraceIdentifier.ToString();
    }

    /// <summary>
    /// 账户列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageDto<AccountItemDto>> GetList([FromQuery] AccountListRequest request)
    {
        return await _service.GetList(request);
    }
    /// <summary>
    /// 禁止登录
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task AccountForbidLogin(Guid accountId)
    {
        await _service.ForbidLogin(accountId);
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task CreateUser(CreateUserModel model)
    {
        await _service.AddAccount(model.UserName, model.Password, model.Email??"");
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task UpdatePwd(UpdatePwdModel model)
    {
        await _service.ChangePassword(model.id, model.NewPwd);
    }
}