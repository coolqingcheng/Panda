using Panda.Entity.DataModels;
using Panda.Tools.Auth;

namespace Panda.Services.Account;

public interface IAccountService
{
    Task<string> Test();

    Task<AuthResult> LoginAsync(string userName, string password);
}