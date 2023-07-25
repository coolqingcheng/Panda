using PandaSite.Repositories.Sys;

namespace PandaSite.Services.Sys;

public class AccountRoleService
{
    private readonly AccountRoleRepository _accountRoleRepository;

    public AccountRoleService(AccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }
}