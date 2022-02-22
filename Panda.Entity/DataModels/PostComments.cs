using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class PostComments : PandaBaseTable
{
    public virtual Posts Post { get; set; }

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
}