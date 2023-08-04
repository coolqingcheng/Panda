using Microsoft.EntityFrameworkCore.ChangeTracking;
using Panda.Models.Data.Blogs;
using Panda.Models.Data.ModelConfigs;
using PandaTools.Auth;

namespace Panda.Models.Data;

public class PandaDbContext : AuthDbContext<Accounts>
{
    public PandaDbContext(DbContextOptions<PandaDbContext> options) : base(options)
    {
        StartChangeListen();
    }

    private void StartChangeListen()
    {
        ChangeTracker.StateChanged += UpdateTimestamps;
        ChangeTracker.Tracked += UpdateTimestamps;
    }

    static void UpdateTimestamps(object? sender, EntityEntryEventArgs e)
    {
        if (e.Entry.Entity is not BaseTimeTable model) return;
        switch (e.Entry.State)
        {
            case EntityState.Detached:
                break;
            case EntityState.Unchanged:
                break;
            case EntityState.Deleted:
                break;
            case EntityState.Modified:
                model.UpdateTime = DateTime.Now;
                break;
            case EntityState.Added:
                model.CreateTime = DateTime.Now;
                break;
        }
    }

    public DbSet<Posts> Posts { get; set; }

    public DbSet<PostTags> PostTags { get; set; }

    public DbSet<PostTagRelation> PostTagRelations { get; set; }

    public DbSet<PostCates> PostCates { get; set; }

    public DbSet<PostCateRelation> PostCateRelations { get; set; }

    public DbSet<PostComments> PostComments { get; set; }

    public DbSet<FriendLink> FriendLinks { get; set; }


    /// <summary>
    ///     系统配置
    /// </summary>
    public DbSet<SysConfig> SysConfig { get; set; }

    /// <summary>
    ///     资源文件
    /// </summary>
    public DbSet<SysResource> SysResources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ConfigAccount();
        modelBuilder.PostConfig();
    }
}