using Panda.Entity.DataModels;
using Panda.Tools.Auth;

namespace Panda.Services.Account;

public interface IAccountService
{
    Task InitAsync();

    Task<AuthResult> LoginAsync(string userName, string password);
}