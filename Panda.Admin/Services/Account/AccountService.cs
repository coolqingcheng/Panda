using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Panda.Admin.Models.Request;
using Panda.Admin.Repositorys;
using Panda.Entity.Responses;
using Panda.Tools.Auth;
using Panda.Tools.Auth.Models;
using Panda.Tools.Auth.Request;
using Panda.Tools.Auth.Response;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using Panda.Tools.Security;

namespace Panda.Admin.Services.Account;

public class AccountService<TU> : IAccountService<TU> where TU : Accounts, new()
{
    private readonly AccountRepository<TU> _accountRepository;
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(AccountRepository<TU> accountRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var account = await _accountRepository.Where<TU>(a => a.Email == email)
            .FirstOrDefaultAsync();
        var result = new AuthResult();
        if (account == null)
        {
            result.Message = "账号和密码不匹配";
            return result;
        }

        if (account.IsDisable)
        {
            result.Message = "账户已经被禁用";
            return result;
        }

        if (account.LockedTime > DateTimeOffset.Now)
        {
            result.Message = "用户已经锁定";
            return result;
        }

        if (IdentitySecurity.VerifyHashedPassword(account.Passwd, password) == false)
        {
            result.Message = "账号和密码不匹配";
            await _accountRepository.LoginFailAsync(account);
            return result;
        }

        result.IsSuccess = true;
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


    public async Task ChangePwdAsync(string oldPwd, string newPwd)
    {
        var account = await GetCurrentAccount();
        account.IsNullThrow("登录信息读取失败，请重新登录！");
        if (IdentitySecurity.VerifyHashedPassword(account!.Passwd, oldPwd) == false) throw new UserException("旧密码错误！");

        account.Passwd = IdentitySecurity.HashPassword(newPwd);
        await _accountRepository.SaveAsync();
        await SignOutAsync();
    }

    public async Task<TU?> GetCurrentAccount()
    {
        return await _accountRepository.GetCurrentAccountsAsync();
    }

    public async Task InitAdminPassword()
    {
        var account = await _accountRepository.Where<TU>(a => a.Email == "qingchengcode@qq.com").FirstOrDefaultAsync();
        if (account != null)
        {
            account.Passwd = IdentitySecurity.HashPassword("123456.");
            await _accountRepository.SaveAsync();
        }
    }

    public async Task CreateAdminAccount(CreateAdminAccountRequest request)
    {
        await _accountRepository.InitAccountAsync(request);
    }

    public async Task<PageDto<AccountResp>> GetAccountList(AccountReq req)
    {
        var query = _accountRepository.Queryable<TU>();
        var list = await query.Page(req).OrderByDescending(a => a.LastLoginTime)
            .ProjectToType<AccountResp>()
            .ToListAsync();
        return new PageDto<AccountResp>
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public  Task<bool> CheckAdminAccountExistAsync()
    {
        return  _accountRepository.CheckAdminAccountExistAsync();
    }

    public async Task Disable(Guid accountId, bool status)
    {
        var account = await _accountRepository.Where<TU>(a => a.Id == accountId).FirstOrDefaultAsync();
        if (account != null)
        {
            account.IsDisable = status;
            if (status == false) account.LockedTime = DateTimeOffset.Now.AddSeconds(-1);

            await _accountRepository.SaveAsync();
        }
    }

    public async Task SignOutAsync()
    {
        if (_httpContextAccessor.HttpContext != null)
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}