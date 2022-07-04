using Microsoft.AspNetCore.Mvc;
using Panda.Admin.Admin.Models.Permission;
using Panda.Admin.Services.Permission;
using Panda.Tools.Auth.Permission;
using Panda.Tools.Auth.Permission.Models;

namespace Panda.Admin.Controllers;

/// <summary>
/// 权限控制
/// </summary>
public class PermissionController : AdminController
{
    private readonly IPermissionUtils _permissionUtils;

    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionUtils permissionUtils)
    {
        _permissionUtils = permissionUtils;
    }
    /// <summary>
    /// 获取所有的权限
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<PermissionGroup> GetAll()
    {
        return _permissionUtils.GetAllPermission();
    }

    /// <summary>
    /// 设置用户权限
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task AccountSetPermission(AccountSetPermissionRequest request)
    {
        await _permissionService.AccountSetPermission(request);
    }

    /// <summary>
    /// 获取用户权限
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<string>> GetPermissions(Guid accountId)
    {
        var hasSet = await _permissionService.GetAccountPermission(accountId);
        return hasSet.ToList();
    }
}