using Panda.Models.Dtos.Account;

namespace Panda.Repositoies.Sys;

public class AccountRepository : BaseRepository<Accounts,Guid>
{
    public AccountRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Accounts?> GetByUserNameAsync(string userName)
    {
        return await DbContext.Set<Accounts>().FirstOrDefaultAsync(a => a.UserName == userName || a.Email == userName);
    }

    public async Task<bool> CheckUserNameExistAsync(string userName)
    {
        return await DbContext.Set<Accounts>().Where(a => a.UserName == userName || a.Email == userName)
            .AnyAsync();
    }

    /// <summary>
    ///     获取账号已经关联的角色
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public async Task<Accounts?> GetAccountAndRolesByAccountId(Guid accountId)
    {
        return await DbContext.Set<Accounts>().Include(a => a.AccountRoleRelations)
            .FirstOrDefaultAsync(a => a.Id == accountId);
    }

    public async Task<PageDto<AccountItemDto>> GetAccountList(AccountListRequest request)
    {
        var query = DbContext.Set<Accounts>();
        var res = await DbContext.Set<Accounts>()
            .Select(a => new AccountItemDto
            {
                Id = a.Id,
                UserName = a.UserName,
                Email = a.Email,
                CreateTime = a.CreateTime,
                LastUpdateTime = a.UpdateTime,
                LastLoginTime = DbContext.Set<AccountLoginRecord>().Where(x => x.Account == a)
                    .Max(x => x.CreateTime),
                RoleName = a.AccountRoleRelations.Select(x => x.AccountRole.RoleName).ToList(),
                LockedTime = a.LockedTime
            }).ToListAsync();
        return new PageDto<AccountItemDto>(await query.CountAsync(), res);
    }

    /// <summary>
    ///     检查账户表中是否存在账户
    /// </summary>
    /// <returns></returns>
    public Task<bool> ExistAccount()
    {
        return DbContext.Set<Accounts>().AnyAsync();
    }
}