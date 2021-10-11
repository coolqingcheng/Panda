using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Account;

public class AccountRepository:PandaRepository<Accounts>
{
    public AccountRepository(PandaContext context) : base(context)
    {
        
    }
    
}