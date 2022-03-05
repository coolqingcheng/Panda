using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Roles;

public class AccountRoleRepository : PandaRepository<AccountRoleRelations>
{
    public AccountRoleRepository(PandaContext context) : base(context)
    {
        
    }

    public async Task AddRoleRelation(Accounts account, Entity.DataModels.Roles roles)
    {
        await AddAsync(new AccountRoleRelations()
        {
            Account = account,
            Role = roles
        });
    }
}