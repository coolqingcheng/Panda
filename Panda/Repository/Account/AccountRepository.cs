using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Account;

public class AccountRepository : PandaRepository<Accounts>
{
    public AccountRepository(PandaContext context) : base(context)
    {
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


    private bool IsLocked(Accounts account)
    {
        return account.LockedTime > DateTime.Now;
    }

    public async Task LoginSuccessAsync(Accounts account)
    {
        account.LockedTime = DateTime.Now.AddSeconds(-1);
        account.LoginFailCount = 0;
        await _context.SaveChangesAsync();
    }
}