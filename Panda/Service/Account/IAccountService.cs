using Panda.Entity.DataModels;
using Panda.Tools.Auth;

namespace Panda.Services.Account;

public interface IAccountService
{
    Task InitAsync();

    Task<AuthResult> LoginAsync(string email, string password);

    Task ChangePwdAsync(string oldPwd, string newPwd);

    Task<Accounts?> GetCurrentAccount();
}