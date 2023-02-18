using QingCheng.Tools.EFCore;

namespace QingCheng.Site.Data.Entitys;

public class AccountRoles : BaseTableModel<Guid>
{
    public string RoleName { get; set; }

    public List<AccountRoleRelation> AccountRoleRelations { get; set; }
}

public class AccountRoleRelation : BaseTableModel<int>
{
    public Accounts Account { get; set; }

    public AccountRoles AccountRole { get; set; }
}