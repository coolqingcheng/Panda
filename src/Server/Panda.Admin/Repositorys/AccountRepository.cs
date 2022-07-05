using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Panda.Admin.Models.Request;
using Panda.Tools.Auth.Models;
using Panda.Tools.Auth.Repositorys;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using Panda.Tools.Security;

namespace Panda.Admin.Repositorys;

public class AccountRepository
{
    private new readonly DbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountRepository(DbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task InitAccountAsync(CreateAdminAccountRequest request)
    {
        var any = await _context.Set<Accounts>().AnyAsync();
        if (any == false)
        {
            await _context.Set<Accounts>().AddAsync(new()
            {
                UserName = request.UserName,
                NickName = "管理员",
                Passwd = IdentitySecurity.HashPassword(request.Pwd),
                AddTime = DateTime.Now,
                Email = request.Email,
                IsAdmin = true
            });
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new UserException("系统已经初始化，重复操作失败");
        }
    }

    public async Task LoginFailAsync<T>(T account) where T : Accounts
    {
        account.LoginFailCount += 1;
        if (account.LoginFailCount >= 5 && IsLocked(account) == false)
            //大于5次锁定账户
            account.LockedTime = DateTimeOffset.Now.AddMinutes(15);

        await _context.SaveChangesAsync();
    }


    private static bool IsLocked(Accounts account)
    {
        return account.LockedTime > DateTimeOffset.Now;
    }

    public async Task LoginSuccessAsync(Accounts account)
    {
        account.LockedTime = DateTimeOffset.Now.AddSeconds(-1);
        account.LoginFailCount = 0;
        account.LastLoginTime = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetCurrentAccountsAsync<T>() where T : Accounts
    {
        var accountId = _httpContextAccessor.HttpContext?.CurrentAccountId();
        var account = await _context.Set<T>().Where(a => a.Id == accountId).FirstOrDefaultAsync();
        account.IsNullThrow("登录身份验证失败，请重新登录");
        return account!;
    }

    public Task<bool> CheckAdminAccountExistAsync()
    {
        return _context.Set<Accounts>().AnyAsync();
    }
}