using QingCheng.Site.Repositories.Sys;

namespace QingCheng.Site.Services.Sys;

public class AccountRoleService
{
    private readonly AccountRoleRepository _accountRoleRepository;

    public AccountRoleService(AccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }
}