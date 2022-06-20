namespace Panda.Tools.Web.RSS;

public class RssModel
{
    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     链接
    /// </summary>
    public string Link { get; set; }

    /// <summary>
    ///     描述
    /// </summary>
    public string Description { get; set; }

    public List<RssItem> Item { get; set; } = new();
}

public class RssItem
{
    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     链接
    /// </summary>
    public string Link { get; set; }

    /// <summary>
    ///     描述
    /// </summary>
    public string Description { get; set; }
}