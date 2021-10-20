using Panda.Entity.Requests;
using Panda.Tools.Models;

namespace Panda.Entity.Models;

public class ArticleItem
{
    public int Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 摘要
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime AddTime { get; set; }

    public int? AccountId { get; set; }

    public string? AccountName { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public List<ArticleCategories> Categories { get; set; }
}

public class ArticleDetailItem : ArticleItem
{
    /// <summary>
    /// html内容
    /// </summary>
    public string Content { get; set; }
}

public class ArticleCategories
{
    public int Id { get; set; }

    public string CateName { get; set; }
}


public class PostRequest : BasePageRequest
{
}

public class PostCategoryRequest : BasePageRequest
{
    
    public int CategoryId { get; set; }
}