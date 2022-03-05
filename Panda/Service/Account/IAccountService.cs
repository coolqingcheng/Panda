using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Tools.Auth;

namespace Panda.Services.Account;

public interface IAccountService
{
    Task InitAsync();

    Task<AuthResult> LoginAsync(string email, string password);

    Task ChangePwdAsync(string oldPwd, string newPwd);

    Task<Accounts?> GetCurrentAccount();

    Task InitAdminPassword();

    Task<PageDto<AccountResp>> GetAccountList(AccountReq req);


    Task Disable(Guid accountId, bool status);
    
    
}