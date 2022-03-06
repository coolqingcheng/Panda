using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;
using Panda.Tools.Auth.Repositorys;
using Panda.Tools.Extensions;

namespace Panda.Admin.Repositorys;

public class AccountRepository<T> : BaseRepository where T : Accounts
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private new readonly DbContext _context;

    public AccountRepository(DbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LoginFailAsync(T account)
    {
        account.LoginFailCount += 1;
        if (account.LoginFailCount >= 5 && IsLocked(account) == false)
        {
            //大于5次锁定账户
            account.LockedTime = DateTimeOffset.Now.AddMinutes(15);
        }

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

    public async Task<T> GetCurrentAccountsAsync()
    {
        var accountId = _httpContextAccessor.HttpContext?.CurrentAccountId();
        var account = await _context.Set<T>().Where(a => a.Id == accountId).FirstOrDefaultAsync();

        account.IsNullThrow("登录身份验证失败，请重新登录");
        return account!;
    }
}