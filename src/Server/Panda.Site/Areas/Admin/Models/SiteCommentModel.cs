using System.Text.Json.Serialization;

namespace Panda.Site.Areas.Admin.Models;

public class SiteCommentModel
{
    public int Id { get; set; }

    public string NickName { get; set; }

    public string Email { get; set; }

    /// <summary>
    /// 所在区域
    /// </summary>
    public string Region { get; set; }

    public string Ip { get; set; }

    public string Content { get; set; }

    public DateTime AddTime { get; set; }

    [JsonIgnore] public string UA { get; set; }
}

public class AskCommentRequest
{
    public int CommentId { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
}