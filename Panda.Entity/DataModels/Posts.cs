

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Entity.DataModels;

public class Posts:PandaBaseTable
{

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
    /// 修改时间
    /// </summary>
    public DateTime UpdateTime { get; set; }

    public List<PostsCategoryRelations> ArticleCategoryRelations { get; set; }
}