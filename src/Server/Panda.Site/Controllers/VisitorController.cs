using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Site.Models;
using Panda.Site.Services.Site;
using Panda.Tools.Email;
using Panda.Tools.Filter;
using Panda.Tools.Web;

namespace Panda.Site.Controllers;

/// <summary>
/// 访客操作
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class VisitorController : Controller
{
    private readonly IVisitorService _visitor;

    private readonly DbContext _context;

    public VisitorController(IVisitorService visitor, DbContext context)
    {
        _visitor = visitor;
        _context = context;
    }

    /// <summary>
    /// 发送验证码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [EFTransacation]
    public async Task SendVerificationCode(VisitorSendVerificationCode model)
    {
        await _visitor.SendVerificationCode(model.NickName, model.Email);
    }

    /// <summary>
    /// 测试发送密码
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public void Test()
    {
        App.GetService<IEmailSender>()!
            .SendEmail("984587039@qq.com", "盒饭", "测试邮件", "你的验证码是:<strong>10000</strong>");
    }
}