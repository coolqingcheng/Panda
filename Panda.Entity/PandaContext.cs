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

    public DbSet<DicDatas> DicDatas { get; set; }

    public DbSet<FriendlyLinks> FriendlyLinks { get; set; }

    public DbSet<FriendlyLinkRecord> FriendlyLinkRecords { get; set; }

    public DbSet<PostTags> PostTags { get; set; }

    public DbSet<TagsRelation> TagsRelations { get; set; }


    public DbSet<Notices> Notices { get; set; }

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
        modelBuilder.Entity<Accounts>().HasIndex(a => a.Email).IsUnique();

        modelBuilder.Entity<Posts>().Property(a => a.Summary).HasMaxLength(250);
        modelBuilder.Entity<Posts>().Property(a => a.Text).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().Property(a => a.Content).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().HasIndex(a => new { a.Text, a.Title }).IsFullText(fullText: true, parser: "ngram");
        modelBuilder.Entity<Posts>().HasIndex(a => new { a.Id, a.Status });
        modelBuilder.Entity<Posts>().HasIndex(a => a.CustomLink);
        modelBuilder.Entity<Posts>().HasIndex(a => a.UpdateTime);
        modelBuilder.Entity<DicDatas>().Property(a => a.Pid).HasDefaultValue(0);
        modelBuilder.Entity<FriendlyLinks>().Property(a => a.AuditStatus).HasDefaultValue(AuditStatusEnum.unaudit);
        modelBuilder.Entity<Notices>().Property(a => a.IsTop).HasDefaultValue(false);

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

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SaveChangeModifyAddTime();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SaveChangeModifyAddTime()
    {
        foreach (var entry in ChangeTracker.Entries().Where(a => a.State == EntityState.Added))
        {
            if (!entry.CurrentValues.TryGetValue<DateTime>("UpdateTime", out DateTime time)) continue;
            if (time == default)
            {
                entry.CurrentValues["UpdateTime"] = DateTime.Now;
            }

            if (!entry.CurrentValues.TryGetValue<DateTime>("AddTime", out time)) continue;
            if (time == default)
            {
                entry.CurrentValues["AddTime"] = DateTime.Now;
            }

        }
    }
}