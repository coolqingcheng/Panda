using Panda.Models.Data.Blogs;
using Panda.Models.Data.ModelConfigs;
using PandaTools.Auth;

namespace Panda.Models.Data;

public class PandaDbContext : AuthDbContext<Accounts>
{
    public DbSet<Posts> Posts { get; set; }

    public DbSet<PostTags> PostTags { get; set; }

    public DbSet<PostTagRelation> PostTagRelations { get; set; }

    public DbSet<PostCates> PostCates { get; set; }

    public DbSet<PostCateRelation> PostCateRelations { get; set; }

    public DbSet<PostComments> PostComments { get; set; }

    public DbSet<FriendLink> FriendLinks { get; set; }


    /// <summary>
    /// 系统配置
    /// </summary>
    public DbSet<SysConfig> SysConfig { get; set; }
    /// <summary>
    /// 资源文件
    /// </summary>
    public DbSet<SysResource> SysResources { get; set; }

    public PandaDbContext(DbContextOptions<PandaDbContext> options) : base(options)
    {

    }

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
        modelBuilder.SystemModelConfig();


    }

}