using Microsoft.AspNetCore.Mvc;
using Panda.Admin.Attributes;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Permission;

namespace Panda.Admin.Controllers;

/// <summary>
/// 角色
/// </summary>
[PermissionGroup("角色管理")]
public class RoleController : AdminController
{
    
}