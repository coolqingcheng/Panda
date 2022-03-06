using Panda.Entity.Responses;
using Panda.Tools.Auth;
using Panda.Tools.Auth.Models;
using Panda.Tools.Auth.Request;
using Panda.Tools.Auth.Response;

namespace Panda.Admin.Services.Account;

public interface IAccountService<TU> where TU : Accounts
{
    // Task InitAsync();

    Task<AuthResult> LoginAsync(string email, string password);

    Task ChangePwdAsync(string oldPwd, string newPwd);


    Task InitAdminPassword();

    Task InitAccount();


    Task Disable(Guid accountId, bool status);


    Task<TU?> GetCurrentAccount();

    Task<PageDto<AccountResp>> GetAccountList(AccountReq req);
}