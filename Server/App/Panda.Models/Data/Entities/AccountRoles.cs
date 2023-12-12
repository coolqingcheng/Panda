﻿namespace Panda.Models.Data.Entities;

public class AccountRoles : BaseEntity<Guid>
{
    public string RoleName { get; set; }

    public List<AccountRoleRelation> AccountRoleRelations { get; set; } = new();
}

public class AccountRoleRelation : BaseEntity<int>
{
    public Accounts Account { get; set; }

    public AccountRoles AccountRole { get; set; }
}