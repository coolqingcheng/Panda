using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;
using Panda.Tools.Auth.Repositorys;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using Panda.Tools.Security;

namespace Panda.Admin.Repositorys;

public class AccountRepository<T> : BaseRepository where T : Accounts, new()
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private new readonly DbContext _context;

    public AccountRepository(DbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task InitAccountAsync()
    {
        var any = await _context.Set<T>().AnyAsync();
        if (any == false)
        {
            await _context.Set<T>().AddAsync(new T()
            {
                UserName = "管理员",
                NickName = "管理员",
                Passwd = IdentitySecurity.HashPassword("admin"),
                AddTime = DateTimeOffset.Now,
                Email = "admin"
            });
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new UserException("系统已经初始化，重复操作失败");
        }
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