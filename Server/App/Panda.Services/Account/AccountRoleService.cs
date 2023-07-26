
using Panda.Repositoies.Sys;

namespace Panda.Services.Sys;

public class AccountRoleService
{
    private readonly AccountRoleRepository _accountRoleRepository;

    public AccountRoleService(AccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }
}