using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Panda.Entity.Requests;
using Panda.Tools.Models;

namespace Panda.Models;

public class CommentModels
{
}

public class CommentRequest
{
    [Required(ErrorMessage = "PostId不能为空")]
    public int PostId { get; set; }

    [Required(ErrorMessage = "评论信息不能为空")]
    public string Message { get; set; }

    public int? CommentId { get; set; }
}

public class GetCommentRequest:BasePageRequest
{
    [Required]
    public int PostId { get; set; }
}

public class CommentItem
{
    public int Id { get; set; }

    public string NickName { get; set; }

    public string Browser { get; set; }

    public string Os { get; set; }

    public string Device { get; set; }

    public string Content { get; set; }

    public DateTimeOffset AddTime { get; set; }

    public int Index { get; set; }

    [JsonIgnore]
    public string UserAgent { get; set; }

    /// <summary>
    /// 回复某Id
    /// </summary>
    public int? ReplyId { get; set; }

    public string AnswerNickName { get; set; }

    public List<CommentItem> Children { get; set; } = new();
}