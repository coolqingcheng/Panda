using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class PostComments : PandaBaseTable
{
    /// <summary>
    /// 文章
    /// </summary>
    public virtual Posts Post { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public virtual Accounts Account { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 回复某条评论
    /// </summary>
    public PostComments? AnswerComment { get; set; }

    /// <summary>
    /// 浏览器UA
    /// </summary>
    [StringLength(1000)]
    public string UserAgent { get; set; }

    /// <summary>
    /// Ip地址
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    /// 上级评论
    /// </summary>
    public PostComments? ParentComment { get; set; }
}