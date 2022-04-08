using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;

namespace Panda.Tools.Auth;

public abstract class AppContext<TU> : DbContext where TU : Accounts
{
    public AppContext(DbContextOptions option) : base(option)
    {
    }

    public DbSet<TU> Accounts { get; set; }

    public DbSet<DicDatas> DicDatas { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SaveChangeModifyAddTime();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        SaveChangeModifyAddTime();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SaveChangeModifyAddTime();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        SaveChangeModifyAddTime();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SaveChangeModifyAddTime()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified)
            {
                if (!entry.CurrentValues.TryGetValue<DateTimeOffset>("UpdateTime", out var time)) continue;
                if (time == default) entry.CurrentValues["UpdateTime"] = DateTimeOffset.Now;
            }

            if (entry.State == EntityState.Added)
            {
                if (!entry.CurrentValues.TryGetValue<DateTimeOffset>("AddTime", out var time)) continue;
                if (time == default) entry.CurrentValues["AddTime"] = DateTimeOffset.Now;
            }
        }
    }
}