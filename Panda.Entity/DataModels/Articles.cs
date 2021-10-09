using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Articles:PandaBaseTable
{

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 富文本
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 富文本的纯文字
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public int AccountId { get; set; }
}