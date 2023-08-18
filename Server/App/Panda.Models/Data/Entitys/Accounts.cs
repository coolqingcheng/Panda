using Panda.Models.Data.ModelConfigs;

namespace Panda.Models.Data.Entitys;

public class Accounts : BaseAccount
{
    public List<AccountRoleRelation> AccountRoleRelations { get; set; }
}

public static class AccountModelCreating
{
    public static void ConfigAccount(this ModelBuilder builder)
    {
        builder.Entity<Accounts>().HasKey(a => a.Id);
        builder.Entity<Accounts>().Property(a => a.UserName).IsRequired().HasMaxLength(20);
        builder.Entity<Accounts>().Property(a => a.Pwd).IsRequired().HasMaxLength(2000);
        builder.Entity<Accounts>().Property(a => a.Email).HasMaxLength(200);
        builder.Entity<Accounts>().HasMany(a => a.AccountRoleRelations)
            .WithOne(a => a.Account);

        builder.Entity<AccountRoles>().HasMany(a => a.AccountRoleRelations)
            .WithOne(a => a.AccountRole);
        builder.Entity<AccountRoles>().Property(a => a.RoleName)
            .HasMaxLength(20)
            .IsUnicode();
    }
}