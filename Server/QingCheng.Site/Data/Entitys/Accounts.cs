﻿using Microsoft.EntityFrameworkCore;
using QingCheng.Site.Data.ModelConfigs;
using QingCheng.Tools.Auth.Models;
using QingCheng.Tools.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Data.Entitys;

public class Accounts : BaseAccount
{
    public List<AccountRoleRelation> AccountRoleRelations { get; set; }

}

public static class AccountModelCreating
{

    public static void ConfigAccount(this ModelBuilder builder)
    {
        builder.SetDateTimeConfig<Accounts>().HasKey(a => a.Id);
        builder.SetDateTimeConfig<Accounts>().Property(a => a.UserName).IsRequired().HasMaxLength(20);
        builder.SetDateTimeConfig<Accounts>().Property(a => a.Pwd).IsRequired().HasMaxLength(2000);
        builder.SetDateTimeConfig<Accounts>().Property(a => a.Email).HasMaxLength(200);
        builder.SetDateTimeConfig<Accounts>().HasMany(a => a.AccountRoleRelations)
            .WithOne(a => a.Account);

        builder.SetDateTimeConfig<AccountRoles>().HasMany(a => a.AccountRoleRelations)
            .WithOne(a => a.AccountRole);
        builder.SetDateTimeConfig<AccountRoles>().Property(a => a.RoleName)
            .HasMaxLength(20)
            .IsUnicode();
    }
}