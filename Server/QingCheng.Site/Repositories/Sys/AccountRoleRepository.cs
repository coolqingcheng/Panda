using QingCheng.Site.Data.Entitys;

namespace QingCheng.Site.Repositories.Sys;

public class AccountRoleRepository : BaseRepository<AccountRoles>
{
    public AccountRoleRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<AccountRoles>> GetAccountRoleListByRoleIds(List<Guid> roleIds)
    {
        return await DbContext.Set<AccountRoles>().Where(a => roleIds.Contains(a.Id))
            .ToListAsync();
    }

    public async Task AddRoleRelationAsync(Accounts account, List<AccountRoles> rolesList)
    {
        await DbContext.Set<AccountRoleRelation>().Where(a=>a.Account==account).ExecuteDeleteAsync();
        account.AccountRoleRelations.AddRange(rolesList.Select(a => new AccountRoleRelation()
        {
            AccountRole = a,
            Account = account
        }));
        await DbContext.SaveChangesAsync();
    }
}