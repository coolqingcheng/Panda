using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Tools.Auth;
using Panda.Tools.Auth.Models;

namespace Panda.Entity;

public class PandaContext : AppContext<Accounts>
{
    public DbSet<Posts> Posts { get; set; }

    public DbSet<Categorys> Categories { get; set; }

    public DbSet<PostsCategoryRelations> ArticleCategoryRelations { get; set; }

    public DbSet<AuditLogs> AuditLogs { get; set; }

    public DbSet<Pages> Pages { get; set; }

    public DbSet<FriendlyLinks> FriendlyLinks { get; set; }

    public DbSet<FriendlyLinkRecord> FriendlyLinkRecords { get; set; }

    public DbSet<PostTags> PostTags { get; set; }

    public DbSet<TagsRelation> TagsRelations { get; set; }


    public DbSet<Notices> Notices { get; set; }


    #region Wiki

    public DbSet<Wiki> Wikis { get; set; }

    public DbSet<WikiDoc> WikiDocs { get; set; }

    public DbSet<WikiGroup> WikiGroups { get; set; }

    #endregion

    public PandaContext(DbContextOptions<PandaContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Accounts>().HasIndex(a => a.Email).IsUnique();

        modelBuilder.Entity<Posts>().Property(a => a.Summary).HasMaxLength(250);
        modelBuilder.Entity<Posts>().Property(a => a.Text).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().Property(a => a.Content).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().Property(a => a.MarkDown).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().HasIndex(a => new { a.Text, a.Title }).IsFullText(fullText: true, parser: "ngram");
        modelBuilder.Entity<Posts>().HasIndex(a => new { a.Id, a.Status });
        modelBuilder.Entity<Posts>().HasIndex(a => a.CustomLink).IsUnique();
        modelBuilder.Entity<Posts>().HasIndex(a => a.UpdateTime);
        modelBuilder.Entity<DicDatas>().Property(a => a.Pid).HasDefaultValue(0);
        modelBuilder.Entity<FriendlyLinks>().Property(a => a.AuditStatus).HasDefaultValue(AuditStatusEnum.unaudit);
        modelBuilder.Entity<Notices>().Property(a => a.IsTop).HasDefaultValue(false);
        modelBuilder.Entity<WikiDoc>().Property(a => a.WikiContent).HasColumnType("longtext");
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
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Modified)
            {
                if (!entry.CurrentValues.TryGetValue<DateTimeOffset>("UpdateTime", out var time)) continue;
                if (time == default)
                {
                    entry.CurrentValues["UpdateTime"] = DateTimeOffset.Now;
                }
            }

            if (entry.State == EntityState.Added)
            {
                if (!entry.CurrentValues.TryGetValue<DateTimeOffset>("AddTime", out var time)) continue;
                if (time == default)
                {
                    entry.CurrentValues["AddTime"] = DateTimeOffset.Now;
                }
            }
        }
    }
}