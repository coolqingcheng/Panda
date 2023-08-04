﻿using MediatR;
using Microsoft.Extensions.Logging;
using Panda.Models.Dtos.Account;
using Panda.Models.Dtos.Auth;
using Panda.Repositoies.Sys;
using PandaTools.Helper;

namespace Panda.Services.Sys;

public class AccountService
{
    private readonly AccountRepository _accountRepository;

    private readonly AccountRoleRepository _accountRoleRepository;

    private readonly ILogger _logger;


    private readonly IMediator _mediator;

    public AccountService(IMediator mediator, ILogger<AccountService> logger, AccountRepository accountRepository,
        AccountRoleRepository accountRoleRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
    }

    public async Task<AuthResult<Accounts>> LoginAsync(string userName, string pwd)
    {
        pwd = pwd.GetSHA256();
        var account = await _accountRepository.GetByUserNameAsync(userName);
        var res = new AuthResult<Accounts>(false);
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
        }
        else if (account.LoginFailCount > 5)
        {
            account.LoginFailCount = 0;
            account.LockedTime = DateTime.Now.AddMinutes(15);
        }

        else
        {
            res.Data = account;
            res.IsOk = true;
        }

        await _accountRepository.UpdateAsync(account!);


        return res;
    }

    public async Task<BaseAuthResult> AddAccount(string userName, string pwd, string email)
    {
        var res = new BaseAuthResult(isOk: false, message: "");
        var exist = await _accountRepository.CheckUserNameExistAsync(userName);
        if (exist == false)
        {
            await _accountRepository.AddAsync(new Accounts
            {
                UserName = userName,
                Pwd = pwd.GetSHA256(),
                Email = email,
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid()
            });
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
    ///     修改密码[需要验证当前密码]
    /// </summary>
    /// <param name="id"></param>
    /// <param name="currPwd"></param>
    /// <param name="newPwd"></param>
    /// <returns></returns>
    /// <exception cref="UserException"></exception>
    public async Task ChangePassword(Guid id, string currPwd, string newPwd)
    {
        var account = await _accountRepository.FindByIdAsync(id);
        if (account != null)
        {
            if (account.Pwd != currPwd.GetSHA256()) throw new UserException("当前密码错误");

            account.Pwd = newPwd.GetSHA256();
            await _accountRepository.UpdateAsync(account);
        }
    }

    /// <summary>
    ///     修改密码
    /// </summary>
    /// <param name="id"></param>
    /// <param name="newPwd"></param>
    /// <returns></returns>
    public async Task ChangePassword(Guid id, string newPwd)
    {
        var account = await _accountRepository.FindByIdAsync(id);
        if (account != null)
        {
            account.Pwd = newPwd.GetSHA256();
            await _accountRepository.UpdateAsync(account);
        }
    }

    /// <summary>
    ///     获取用户列表
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<PageDto<AccountItemDto>> GetList(AccountListRequest model)
    {
        return await _accountRepository.GetAccountList(model);
    }

    /// <summary>
    ///     设置角色
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="roleIds"></param>
    public async Task SetRole(Guid accountId, List<Guid> roleIds)
    {
        var account = await _accountRepository.GetAccountAndRolesByAccountId(accountId);
        if (account == null) return;

        var roleList = await _accountRoleRepository.GetAccountRoleListByRoleIds(roleIds);
        if (roleList.Count == 0) return;

        await _accountRoleRepository.AddRoleRelationAsync(account, roleList);
    }


    /// <summary>
    ///     禁止登录，锁定100年，如果已经锁定，那么解锁
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    public async Task ForbidLogin(Guid accountId)
    {
        var account = await _accountRepository.FindByIdAsync(accountId);
        if (account != null)
        {
            account.LockedTime = account.LockedTime > DateTime.Now
                ? DateTime.Now.AddSeconds(-1)
                : DateTime.Now.AddYears(100);
            await _accountRepository.UpdateAsync(account);
        }
    }

    /// <summary>
    ///     检查系统初始化状态
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CheckSysInitStatus()
    {
        return await _accountRepository.ExistAccount();
    }
}