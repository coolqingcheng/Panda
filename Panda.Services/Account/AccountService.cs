using System.Globalization;

namespace Panda.Services.Account;

public class AccountService:IAccountService
{
    public async Task<string> Test()
    {
        return await Task.FromResult(DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
    }
}