using Microsoft.AspNetCore.Mvc;
using Panda.Admin.Admin.Models.Permission;
using Panda.Admin.Models.Accounts;
using Panda.Admin.Services.Account;
using Panda.Admin.Services.Permission;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Permission;
using Panda.Tools.Auth.Permission.Models;
using Panda.Tools.Web;

namespace Panda.Admin.Controllers;

/// <summary>
/// 权限控制
/// </summary>
[PermissionGroup("权限控制")]
public class PermissionController : AdminController
{
    private readonly IPermissionUtils _permissionUtils;

    private readonly IPermissionService _permissionService;

    private readonly IAccountService _accountService;

    public PermissionController(IPermissionUtils permissionUtils, IAccountService accountService, IPermissionService permissionService)
    {
        _permissionUtils = permissionUtils;
        _accountService = accountService;
        _permissionService = permissionService;
    }
    /// <summary>
    /// 获取所有的权限
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Permission("查看所有用户权限")]
    public async Task<IEnumerable<PermissionGroup>> GetAllAsync(Guid accountId)
    {
        var list = _permissionUtils.GetAllPermission().ToList();
        var hasSet = await _permissionService.GetAccountPermission(accountId);
        var isAdmin = await _accountService.IsAdmin(accountId);
        foreach (var item in list)
        {
            foreach (var children in item.List)
            {
                if (hasSet.Contains(children.Key) || isAdmin)
                {
                    children.IsGrant = true;
                }
            }
        }
        return list;
    }

    /// <summary>
    /// 修改用户权限
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Permission("修改用户权限")]
    public async Task AccountSetPermission(AccountSetPermissionRequest request)
    {
        await _permissionService.AccountSetPermission(request);
    }

    /// <summary>
    /// 获取当前用户权限
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [NoPermission]
    public async Task<AccountPermissionModel> GetPermissions()
    {
        var hasSet = await _permissionService.GetAccountPermission(App.GetAccountId());
        var isAdmin = App.IsAdmin();
        if (isAdmin)
        {
            hasSet = _permissionUtils.GetAllPermission().SelectMany(a => a.List.Select(b => b.Key)).ToHashSet();
        }
        return new AccountPermissionModel()
        {
            IsAdmin = isAdmin,
            Permissions = hasSet.ToList()
        };
    }
}