using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Panda.Admin.Models;
using Panda.Admin.Models.Accounts;
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

public class AccountService : IAccountService
{
    private readonly DbContext _dbContext;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly AccountRepository _accountRepository;

    public AccountService(DbContext context,
        IHttpContextAccessor httpContextAccessor, AccountRepository accountRepository)
    {
        _dbContext = context;
        _httpContextAccessor = httpContextAccessor;
        _accountRepository = accountRepository;
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var account = await _dbContext.Set<Accounts>().Where(a => a.Email == email || a.UserName == email)
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
        if (account.AccountType != AccountType.BackgroundManage)
        {
            throw new UserException("访问无权限");
        }
        result.IsSuccess = true;
        await _accountRepository.LoginSuccessAsync(account);
        var identity = new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Name, account.UserName),
            new("Id", account.Id.ToString()),
            new("IsAdmin", account.IsAdmin.ToString())
        }, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(identity);

        //后台全部走ajax请求，请求头header必须带上 X-Requested-With=XMLHttpRequest，否则中间件会执行重定向
        if (_httpContextAccessor.HttpContext != null)
            await _httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);
        return result;
    }


    public async Task ChangeSelfPasswordAsync(ChangePwdRequest request)
    {
        var account = await _dbContext.Set<Accounts>().Where(a => a.Id == request.Id).FirstOrDefaultAsync();
        if (account == null)
        {
            throw new UserException("账号未找到");
        }

        account.IsNullThrow("登录信息读取失败，请重新登录！");
        if (IdentitySecurity.VerifyHashedPassword(account!.Passwd, request.OldPwd) == false)
            throw new UserException("旧密码错误！");

        account.Passwd = IdentitySecurity.HashPassword(request.NewPwd);
        await _dbContext.SaveChangesAsync();
        await SignOutAsync();
    }


    public async Task ChangeAccountPasswordAsync(Guid accountId, string newPassword)
    {
        var account = await _dbContext.Set<Accounts>().Where(a => a.Id == accountId).FirstOrDefaultAsync();
        if (account == null)
        {
            throw new UserException("账号未找到");
        }

        account.Passwd = IdentitySecurity.HashPassword(newPassword);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TU?> GetCurrentAccount<TU>() where TU : Accounts, new()
    {
        return await _accountRepository.GetCurrentAccountsAsync<TU>();
    }

    public async Task CreateAdminAccount(CreateAdminAccountRequest request)
    {
        await _accountRepository.InitAccountAsync(request);
    }

    public async Task<PageDto<AccountResp>> GetAccountList(AccountReq req)
    {
        var query = _dbContext.Set<Accounts>().AsQueryable();
        var list = await query.Page(req).OrderByDescending(a => a.LastLoginTime)
            .ProjectToType<AccountResp>()
            .ToListAsync();
        return new PageDto<AccountResp>
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public Task<bool> CheckAdminAccountExistAsync()
    {
        return _accountRepository.CheckAdminAccountExistAsync();
    }

    public async Task Disable(Guid accountId, bool status)
    {
        var account = await _dbContext.Set<Accounts>().Where(a => a.Id == accountId).FirstOrDefaultAsync();
        if (account != null)
        {
            account.IsDisable = status;
            if (status == false) account.LockedTime = DateTimeOffset.Now.AddSeconds(-1);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task SignOutAsync()
    {
        if (_httpContextAccessor.HttpContext != null)
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task CreateAccount(CreateAccountModel model)
    {
        await CheckAccount(model);
        await _dbContext.Set<Accounts>().AddAsync(new Accounts()
        {
            Email = model.Email,
            UserName = model.UserName,
            NickName = model.UserName,
            IsAdmin = false,
            Passwd = IdentitySecurity.HashPassword(model.Password),
            AccountType = model.AccountType
        });
        await _dbContext.SaveChangesAsync();
    }

    private async Task CheckAccount(CreateAccountModel model)
    {
        var any = await _dbContext.Set<Accounts>().Where(a => a.Email == model.Email).AnyAsync();
        if (any)
        {
            throw new UserException("邮箱已经存在");
        }

        any = await _dbContext.Set<Accounts>().Where(a => a.UserName == model.UserName).AnyAsync();
        if (any)
        {
            throw new UserException("用户名已经存在");
        }
    }

    public async Task<bool> IsAdmin(Guid AccountId)
    {
        return await _dbContext.Set<Accounts>().Where(a => a.Id == AccountId && a.IsAdmin).AnyAsync();
    }

    public async Task EditAccount(CreateAccountModel model)
    {
        var account = await _dbContext.Set<Accounts>().Where(a => a.Email == model.Email).FirstOrDefaultAsync();
        if (account != null)
        {
            if (account.Id != model.Id)
            {
                throw new UserException("邮箱被其他的用户使用了！");
            }
        }

        account = await _dbContext.Set<Accounts>().Where(a => a.UserName == model.UserName).FirstOrDefaultAsync();
        if (account != null)
        {
            if (account.Id != model.Id)
            {
                throw new UserException("用户名被其他的用户使用了！");
            }
        }

        account = await _dbContext.Set<Accounts>().Where(a => a.Id == model.Id).FirstAsync();
        model.Adapt(account);
        if (string.IsNullOrWhiteSpace(model.Password) == false)
        {
            account.Passwd = IdentitySecurity.HashPassword(model.Password);
        }

        await _dbContext.SaveChangesAsync();
    }
}