using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QingCheng.Site.Api.Auth.Models;
using QingCheng.Site.Data;
using QingCheng.Site.Data.Entitys;
using QingCheng.Tools.Helper;
using QingCheng.Site.Models;
using QingCheng.Site.Models.Auth;
using QingCheng.Site.Auth.Events;

namespace QingCheng.WebApi.Services;

public class AccountService
{
    private readonly DbContext _context;


    private readonly IMediator _mediator;

    private readonly ILogger _logger;

    public AccountService(DbContext context, IMediator mediator, ILogger<AccountService> logger)
    {
        _context = context;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<AuthResult<Accounts>> LoginAsync(string userName, string pwd)
    {
        pwd = pwd.GetSHA256();
        var account = await _context.Set<Accounts>().Where(a => a.UserName == userName || a.Email == userName)
            .FirstOrDefaultAsync();
        var res = new AuthResult<Accounts>(isOk: false);
        if (account == null)
        {
            res.Message = "账号和密码不匹配";
            _logger.LogError($"账号:{userName} 密码: {pwd} 尝试登录，失败！");
        }
        else if (account.LockedTime > DateTime.Now)
        {
            res.Message = "账号已经锁定！";
        }
        else if (account.Pwd != pwd)
        {
            res.Message = "账号和密码不匹配";
            account.LoginFailCount += 1;
            await _context.SaveChangesAsync();
        }
        else if (account.LoginFailCount > 5)
        {
            account.LoginFailCount = 0;
            account.LockedTime = DateTime.Now.AddMinutes(15);
            await _context.SaveChangesAsync();
        }

        else
        {
            res.Data = account;
            res.IsOk = true;
        }

        await _mediator.Publish(new LoginEvent()
        {
            Account = account!,
            Message = res.Message,
            isLogin = res.IsOk
        });

        return res;
    }

    public async Task<BaseAuthResult> AddAccount(string userName, string pwd, string email)
    {
        var res = new BaseAuthResult(isOk: false, message: "");
        if (await _context.Set<Accounts>().AnyAsync(a => a.UserName == userName || a.Email == email) == false)
        {
            await _context.Set<Accounts>().AddAsync(new Accounts()
            {
                UserName = userName,
                Pwd = pwd.GetSHA256(),
                Email = email,
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid()
            });
            await _context.SaveChangesAsync();
            res.IsOk = true;
            res.Message = "添加账号成功";
        }
        else
        {
            throw new UserException("账号已经存在！");
        }

        return res;
    }

    /// <summary>
    /// 修改密码[需要验证当前密码]
    /// </summary>
    /// <param name="id"></param>
    /// <param name="currPwd"></param>
    /// <param name="newPwd"></param>
    /// <returns></returns>
    /// <exception cref="UserException"></exception>
    public async Task ChangePassword(Guid id, string currPwd, string newPwd)
    {
        var account = await _context.Set<Accounts>().FirstOrDefaultAsync(a => a.Id == id);
        if (account != null)
        {
            if (account.Pwd != currPwd.GetSHA256())
            {
                throw new UserException("当前密码错误");
            }

            account.Pwd = newPwd.GetSHA256();
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newPwd"></param>
    /// <returns></returns>
    public async Task ChangePassword(Guid id, string newPwd)
    {
        var account = await _context.Set<Accounts>().FirstOrDefaultAsync(a => a.Id == id);
        if (account != null)
        {
            account.Pwd = newPwd.GetSHA256();
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<PageDto<AccountItemModel>> GetList(AccountListRequest model)
    {
        var query = _context.Set<Accounts>();
        var res = await _context.Set<Accounts>()
            .Select(a => new AccountItemModel()
            {
                Id = a.Id,
                UserName = a.UserName,
                Email = a.Email,
                CreateTime = a.CreateTime,
                LastUpdateTime = a.UpdateTime,
                LastLoginTime = _context.Set<AccountLoginRecord>().Where(x => x.Account == a)
                    .Max(x => x.CreateTime),
                RoleName = a.AccountRoleRelations.Select(x => x.AccountRole.RoleName).ToList(),
                LockedTime = a.LockedTime
            }).ToListAsync();
        return new PageDto<AccountItemModel>(await query.CountAsync(), res);
    }

    /// <summary>
    /// 设置角色
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="roleIds"></param>
    public async Task SetRole(Guid accountId, List<Guid> roleIds)
    {
        var account = await _context.Set<Accounts>().Include(a => a.AccountRoleRelations)
            .FirstOrDefaultAsync(a => a.Id == accountId);
        if (account == null)
        {
            return;
        }

        var role = await _context.Set<AccountRoles>().FirstOrDefaultAsync(a => roleIds.Contains(a.Id));
        if (role == null)
        {
            return;
        }

        _context.Set<AccountRoleRelation>().RemoveRange(account.AccountRoleRelations);
        await _context.SaveChangesAsync();
        var roles = await _context.Set<AccountRoles>().Where(a => roleIds.Contains(a.Id)).ToListAsync();
        account.AccountRoleRelations.AddRange(roles.Select(a => new AccountRoleRelation()
        {
            AccountRole = a,
            Account = account
        }));
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// 禁止登录，锁定100年，如果已经锁定，那么解锁
    /// </summary>
    /// <param name="accoundId"></param>
    /// <returns></returns>
    public async Task ForbidLogin(Guid accoundId)
    {
        var account = await _context.Set<Accounts>().FirstOrDefaultAsync(a => a.Id == accoundId);
        if (account != null)
        {
            if (account.LockedTime > DateTime.Now)
            {
                account.LockedTime = DateTime.Now.AddSeconds(-1);
            }
            else
            {
                account.LockedTime = DateTime.Now.AddYears(100);
            }

            await _context.SaveChangesAsync();
        }
    }
}