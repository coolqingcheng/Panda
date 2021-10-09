using Panda.Entity.DataModels;

namespace Panda.Repository.Account;

public class AccountRepository:PandaRepository<Accounts>
{
    public AccountRepository(IFreeSql freeSql) : base(freeSql)
    {
    }
    
}