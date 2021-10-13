using Panda.Entity.Requests;

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

    public int AccountId { get; set; }

    public string AccountName { get; set; }

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
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
}

public class ArticleRequest : PageRequest
{
}

public class ArticleCategoryRequest : PageRequest
{
    
    public int CategoryId { get; set; }
}