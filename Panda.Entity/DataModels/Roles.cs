using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;
using TencentCloud.Cdb.V20170320.Models;

namespace Panda.Entity.DataModels;

public class Roles : KeyGuidTable
{
    public string RoleName { get; set; }
}

public class AccountRoleRelations : PandaBaseTable
{
    public Accounts Account { get; set; }

    public Roles Role { get; set; }
}