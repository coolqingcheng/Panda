using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Panda.Models.Data.Blogs;

namespace Panda.Models.Data.ModelConfigs;

public static class BlogModel
{

    public static EntityTypeBuilder<T> SetDateTimeConfig<T>(this ModelBuilder builder) where T : BaseTimeTable
    {
        builder.Entity<T>().Property(a => a.CreateTime).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        builder.Entity<T>().Property(a => a.UpdateTime).HasDefaultValueSql("CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)");
        return builder.Entity<T>();
    }

    public static void PostConfig(this ModelBuilder builder)
    {
        //Posts

        builder.SetDateTimeConfig<Posts>().HasQueryFilter(a => a.IsDelete == false);

        builder.SetDateTimeConfig<PostTagRelation>().HasQueryFilter(a => a.Post.IsDelete == false);
        builder.SetDateTimeConfig<PostCateRelation>().HasQueryFilter(a => a.Post.IsDelete == false);
        builder.SetDateTimeConfig<PostVisitRecord>().HasQueryFilter(a => a.Post.IsDelete == false);
        builder.SetDateTimeConfig<PostComments>().HasQueryFilter(a => a.Post.IsDelete == false);

        builder.SetDateTimeConfig<Posts>().Property(a => a.Title).IsRequired().HasMaxLength(150);
        builder.SetDateTimeConfig<Posts>().Property(a => a.Content).IsRequired().HasColumnType("text");
        builder.SetDateTimeConfig<Posts>().Property(a => a.Snippet).IsRequired().HasMaxLength(200);
        builder.SetDateTimeConfig<Posts>().HasMany(a => a.TagRelations).WithOne(a => a.Post).OnDelete(DeleteBehavior.Cascade);
        builder.SetDateTimeConfig<Posts>().HasMany(a => a.CateRelations).WithOne(a => a.Post).OnDelete(DeleteBehavior.Cascade);
        builder.SetDateTimeConfig<Posts>().HasMany(a => a.PostComments).WithOne(a => a.Post);

        builder.SetDateTimeConfig<Posts>().HasMany(a => a.VisitRecords).WithOne(a => a.Post)
            .HasForeignKey(a => a.PostId).OnDelete(DeleteBehavior.Cascade);

        builder.SetDateTimeConfig<PostVisitRecord>().Property(a => a.IP).HasMaxLength(50);
        builder.SetDateTimeConfig<PostVisitRecord>().Property(a => a.UA).HasMaxLength(500);
        builder.SetDateTimeConfig<PostVisitRecord>().Property(a => a.UId).HasMaxLength(32);
        //PostTags
        builder.SetDateTimeConfig<PostTags>().Property(a => a.TagName).HasMaxLength(10).IsRequired();

        builder.SetDateTimeConfig<PostTagRelation>()
            .HasQueryFilter(a => a.Post.IsDelete == false)
            .HasOne(a => a.PostTags);
        builder.SetDateTimeConfig<PostTagRelation>().HasOne(a => a.PostTags);

        //PostCate

        builder.SetDateTimeConfig<PostCates>().Property(a => a.CateName).HasMaxLength(20).IsRequired();

        builder.SetDateTimeConfig<PostCates>().HasMany(a => a.PostCateRelations).WithOne(a => a.PostCate);

        builder.SetDateTimeConfig<PostCateRelation>().HasOne(a => a.Post)
            .WithMany(a => a.CateRelations).OnDelete(DeleteBehavior.NoAction);
        builder.SetDateTimeConfig<PostCateRelation>().HasOne(a => a.Post).WithMany(a => a.CateRelations).OnDelete(DeleteBehavior.NoAction);

        
        
        builder.SetDateTimeConfig<SysConfig>().Property(a => a.Key).HasMaxLength(50);
        builder.SetDateTimeConfig<SysConfig>().Property(a => a.Value).HasMaxLength(2000);
        builder.SetDateTimeConfig<SysConfig>().Property(a => a.Description).HasMaxLength(200);
        builder.SetDateTimeConfig<SysConfig>().HasIndex(a => a.Key).IsUnique();
        builder.SetDateTimeConfig<SysConfig>().Property(a => a.GroupName).HasMaxLength(50);
        builder.SetDateTimeConfig<AccountLoginRecord>().Property(a => a.Ip).HasMaxLength(50);
        builder.SetDateTimeConfig<AccountLoginRecord>().Property(a => a.UA).HasMaxLength(500);
        builder.SetDateTimeConfig<SysResource>();

    }
}