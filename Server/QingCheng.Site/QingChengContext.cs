using Microsoft.EntityFrameworkCore;
using QingCheng.Site.Data.Entitys;
using QingCheng.Site.Data.ModelConfigs;
using QingCheng.Tools.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QingCheng.Site.Data.Blogs;

namespace QingCheng.Site;

public class QingChengContext : AuthDbContext<Accounts>
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

    public QingChengContext(DbContextOptions<QingChengContext> options) : base(options)
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