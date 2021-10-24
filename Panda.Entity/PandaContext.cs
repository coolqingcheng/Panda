using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;

namespace Panda.Entity;

public class PandaContext : DbContext
{
    public DbSet<Accounts> Accounts { get; set; }

    public DbSet<Posts> Posts { get; set; }

    public DbSet<Categorys> Categories { get; set; }

    public DbSet<PostsCategoryRelations> ArticleCategoryRelations { get; set; }

    public DbSet<AuditLogs> AuditLogs { get; set; }

    
    public DbSet<Pages> Pages { get; set; }

    public PandaContext(DbContextOptions<PandaContext> options) : base(options)
    {
    }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         base.OnConfiguring(optionsBuilder);
// #if DEBUG
//         optionsBuilder.LogTo(Console.WriteLine);
// #endif
//     }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Accounts>().HasIndex(a => a.UserName).IsUnique();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
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

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        SaveChangeModifyAddTime();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SaveChangeModifyAddTime()
    {
        foreach (var entry in ChangeTracker.Entries().Where(a => a.State == EntityState.Added))
        {
            if (!entry.CurrentValues.TryGetValue<DateTime>("AddTime", out var time)) continue;
            if (time == default)
            {
                entry.CurrentValues["AddTime"] = DateTime.Now;
            }
        }
    }
}