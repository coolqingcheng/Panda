using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Tools.Models;

namespace Panda.Entity.Models;

public class PostItem
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

    /// <summary>
    /// 添加人
    /// </summary>
    public Accounts? Account { get; set; }

    public PostStatus Status { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public List<PostCategories> Categories { get; set; }
}

public class PostDetailItem : PostItem
{
    /// <summary>
    /// html内容
    /// </summary>
    public string Content { get; set; }

    public IEnumerable<PostTagItem> TagItems { get; set; }
    
    
}

public class PostTagItem
{
    public int Id { get; set; }

    public string TagName { get; set; }
}

public class PostCategories
{
    public int Id { get; set; }

    public string CateName { get; set; }
}


public class PostRequest : BasePageRequest
{
    public int CategoryId { get; set; }
}

public class PostCategoryRequest : BasePageRequest
{
    
    public int CategoryId { get; set; }
}