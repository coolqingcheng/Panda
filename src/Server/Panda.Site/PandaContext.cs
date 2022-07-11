using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Tools.Auth;
using Panda.Tools.Auth.Models;

namespace Panda.Site;

public class PandaContext : AppContext<Accounts>
{
    public PandaContext(DbContextOptions<PandaContext> options) : base(options)
    {
    }

    public DbSet<Posts> Posts { get; set; }

    public DbSet<Categorys> Categories { get; set; }

    public DbSet<PostsCategoryRelations> ArticleCategoryRelations { get; set; }

    public DbSet<AuditLogs> AuditLogs { get; set; }

    public DbSet<Entity.DataModels.Pages> Pages { get; set; }

    public DbSet<FriendlyLinks> FriendlyLinks { get; set; }

    public DbSet<FriendlyLinkRecord> FriendlyLinkRecords { get; set; }

    public DbSet<PostTags> PostTags { get; set; }

    public DbSet<TagsRelation> TagsRelations { get; set; }


    public DbSet<Notices> Notices { get; set; }

    /// <summary>
    /// 站点配置
    /// </summary>
    public DbSet<SiteOptions> SiteOptions { get; set; }


    /// <summary>
    /// 访问统计
    /// </summary>
    public DbSet<AccessStatistic> AccessStatistics { get; set; }


    /// <summary>
    /// 权限
    /// </summary>
    public DbSet<SysPermissions> permissions { get; set; }


    /// <summary>
    /// 临时访客
    /// </summary>
    public DbSet<SiteVisitors> SiteVisitors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Accounts>().HasIndex(a => a.Email).IsUnique();

        modelBuilder.Entity<Posts>().Property(a => a.Summary).HasMaxLength(250);
        modelBuilder.Entity<Posts>().Property(a => a.Text).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().Property(a => a.Content).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().Property(a => a.MarkDown).HasColumnType("longtext");
        modelBuilder.Entity<Posts>().HasIndex(a => new { a.Text, a.Title }).IsFullText(true, "ngram");
        modelBuilder.Entity<Posts>().HasIndex(a => new { a.Id, a.Status });
        modelBuilder.Entity<Posts>().HasIndex(a => a.CustomLink).IsUnique();
        modelBuilder.Entity<Posts>().HasIndex(a => a.UpdateTime);
        modelBuilder.Entity<DicDatas>().Property(a => a.Pid).HasDefaultValue(0);
        modelBuilder.Entity<FriendlyLinks>().Property(a => a.AuditStatus).HasDefaultValue(AuditStatusEnum.Unaudit);
        modelBuilder.Entity<Notices>().Property(a => a.IsTop).HasDefaultValue(false);
        modelBuilder.Entity<WikiDoc>().Property(a => a.WikiContent).HasColumnType("longtext");
    }


    #region Wiki

    public DbSet<Wiki> Wikis { get; set; }

    public DbSet<WikiDoc> WikiDocs { get; set; }

    public DbSet<WikiGroup> WikiGroups { get; set; }

    #endregion
}