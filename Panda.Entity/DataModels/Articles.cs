using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Articles:PandaBaseTable
{

    /// <summary>
    /// 标题
    /// </summary>
    [Column(StringLength = 100)]
    public string Title { get; set; }

    /// <summary>
    /// 摘要
    /// </summary>
    [Column(StringLength = 100)]
    public string Summary { get; set; }

    /// <summary>
    /// 富文本
    /// </summary>
    [Column(StringLength = -2)]
    [Required]
    public string Content { get; set; }

    /// <summary>
    /// 富文本的纯文字
    /// </summary>
    [Column(StringLength = -1)]
    public string Text { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public Accounts Account { get; set; }

    public List<ArticleCategoryRelations> ArticleCategoryRelations { get; set; }
}