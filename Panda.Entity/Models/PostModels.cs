using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Tools.Models;
using System.ComponentModel;
using TencentCloud.Ssl.V20191205.Models;

namespace Panda.Entity.Models;

public class PostItem
{
    public int Id { get; set; }

    /// <summary>
    /// 自定义链接
    /// </summary>
    public string CustomLink { get; set; }

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
    public DateTimeOffset AddTime { get; set; }

    /// <summary>
    /// 添加人
    /// </summary>
    public Accounts? Account { get; set; }

    public PostStatus Status { get; set; }

    public string MarkDown { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    public string? Cover { get; set; }

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

    public List<string> Tags
    {
        get { return TagItems.Select(a => a.TagName).ToList(); }
    }
}

public class PostNextItem
{
    public int Id { get; set; }

    public string Title { get; set; }

    public PostNextType Type { get; set; }


    public string CustomLink { get; set; }
}

public enum PostNextType
{
    /// <summary>
    /// 下一条
    /// </summary>
    [Description("下一条")] Next = 1,

    /// <summary>
    /// 上一条
    /// </summary>
    [Description("上一条")] Pre = 0
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