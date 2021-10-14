using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Repository.Account;
using Panda.Tools.Auth;
using Panda.Tools.Security;

namespace Panda.Services.Account;

public class AccountService : IAccountService
{
    private readonly AccountRepository _accountRepository;

    private readonly IdentitySecurity _identitySecurity;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(AccountRepository accountRepository, IdentitySecurity identitySecurity,
        IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _identitySecurity = identitySecurity;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Test()
    {
        return await Task.FromResult(DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
    }

    public async Task<AuthResult> LoginAsync(string userName, string password)
    {
        var account = await _accountRepository.Where(a => a.UserName == userName || a.Email == userName)
            .FirstOrDefaultAsync();
        var result = new AuthResult();
        if (account == null)
        {
            result.Message = "账号和密码不匹配";
            return result;
        }

        if (account.LockedTime > DateTime.Now)
        {
            result.Message = "用户已经锁定";
            return result;
        }

        if (_identitySecurity.VerifyHashedPassword(account.Passwd, password) == false)
        {
            result.Message = "账号和密码不匹配";
            await _accountRepository.LoginFailAsync(account);
            return result;
        }

        await _accountRepository.LoginSuccessAsync(account);
        var identity = new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Name, account.UserName),
            new("Id", account.Id.ToString())
        }, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        //后台全部走ajax请求，请求头header必须带上 X-Requested-With=XMLHttpRequest，否则中间件会执行重定向
        if (_httpContextAccessor.HttpContext != null)
            await _httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);
        return result;
    }
}