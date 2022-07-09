using Microsoft.EntityFrameworkCore;
using Panda.Admin.Models.Accounts;
using Panda.Admin.Services.Account;
using Panda.Entity.DataModels;
using Panda.Tools.Auth.Models;
using Panda.Tools.Email;
using Panda.Tools.Exception;
using Panda.Tools.Helper;
using Panda.Tools.Web;

namespace Panda.Site.Services.Site;

public class VisitorService : IVisitorService
{
    private readonly DbContext _context;

    private readonly IEmailSender _email;

    private readonly IAccountService _account;


    public VisitorService(DbContext context, IEmailSender email, IAccountService account)
    {
        _context = context;
        _email = email;
        _account = account;
    }

    public async Task SendVerificationCode(string nickName, string email)
    {
        var account = await _context.Set<Accounts>()
            .Where(a => a.Email == email && a.AccountType == AccountType.Visitor).FirstOrDefaultAsync();
        var visitor = await _context.Set<SiteVisitors>().Where(a => a.Account.Email == email).FirstOrDefaultAsync();
        if (account == null)
        {
            //创建账号
            await _account.CreateAccount(new CreateAccountModel()
            {
                UserName = nickName,
                Email = email,
                Password = "/",
                AccountType = AccountType.Visitor
            });
            account = await _context.Set<Accounts>()
                .Where(a => a.Email == email && a.AccountType == AccountType.Visitor).FirstOrDefaultAsync();
            visitor = new SiteVisitors()
            {
                Account = account!
            };
            await _context.Set<SiteVisitors>().AddAsync(visitor);
        }

        string code = ValidateCodeHelper.Str(6);
        string content = $"你的验证码是:<strong>{code}</strong>";
        await _email.SendEmail(email, nickName, "注册验证码", content);

        await _context.SaveChangesAsync();
    }

    public async Task ConfirmEmail(string email, string code)
    {
        var visitor = await _context.Set<SiteVisitors>()
            .Include(a => a.Account)
            .FirstOrDefaultAsync(a => a.Account.Email == email && a.Code == code);
        if (visitor != null)
        {
            var guid = Guid.NewGuid().ToString("N");
            visitor.ValidKey = guid;
            await _context.SaveChangesAsync();
            App.SetCookie(IVisitorService.VisitorCookieNickName, visitor.Account.NickName);
            App.SetCookie(IVisitorService.VisitorCookieNickKey, guid);
        }
        else
        {
            throw new UserException("验证码错误");
        }
    }
}