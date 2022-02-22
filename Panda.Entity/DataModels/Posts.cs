using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Panda.Entity.DataModels;

public class Posts : PandaBaseTable
{
    [StringLength(50)] public string? CustomLink { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 摘要
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 富文本
    /// </summary>
    [Required]
    public string Content { get; set; }

    /// <summary>
    /// 富文本的纯文字
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public Accounts Account { get; set; }


    /// <summary>
    /// 状态
    /// </summary>
    public PostStatus Status { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    [DefaultValue(true)]
    public bool AllowComment { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    public string? Cover { get; set; }

    /// <summary>
    /// md文件源
    /// </summary>
    public string MarkDown { get; set; }


    public ICollection<PostsCategoryRelations> ArticleCategoryRelations { get; set; }


    public ICollection<TagsRelation> TagsRelations { get; set; }

    /// <summary>
    /// 评论
    /// </summary>
    public ICollection<PostComments> Comments { get; set; }
}

public enum PostStatus
{
    /// <summary>
    /// 发布
    /// </summary>
    [Description("发布")] Publish = 0,

    /// <summary>
    /// 草稿箱
    /// </summary>
    [Description("草稿箱")] Draft = 1,

    /// <summary>
    /// 隐藏
    /// </summary>
    [Description("隐藏")] Hide = 2
}