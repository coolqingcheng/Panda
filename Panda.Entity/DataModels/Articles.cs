

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panda.Entity.DataModels;

public class Articles:PandaBaseTable
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

    public List<ArticleCategoryRelations> ArticleCategoryRelations { get; set; }
}