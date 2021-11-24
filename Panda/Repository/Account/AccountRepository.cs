using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Tools.Extensions;

namespace Panda.Repository.Account;

public class AccountRepository : PandaRepository<Accounts>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AccountRepository(PandaContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LoginFailAsync(Accounts account)
    {
        if (IsLocked(account))
        {
            account.LoginFailCount = 1;
        }
        else
        {
            account.LoginFailCount += 1;
            if (account.LoginFailCount > 5 && account.LockedTime > DateTime.Now)
            {
                //大于5次锁定账户
                account.LockedTime = DateTime.Now.AddMinutes(15);
            }
        }

        await _context.SaveChangesAsync();
    }


    private static bool IsLocked(Accounts account)
    {
        return account.LockedTime > DateTime.Now;
    }

    public async Task LoginSuccessAsync(Accounts account)
    {
        account.LockedTime = DateTime.Now.AddSeconds(-1);
        account.LoginFailCount = 0;
        await _context.SaveChangesAsync();
    }

    public async Task<Accounts?> GetCurrentAccountsAsync()
    {
        var accountId = _httpContextAccessor.HttpContext?.CurrentAccountId();
        var account = await Where(a => a.Id == accountId).FirstOrDefaultAsync();
        return account;
    }
}