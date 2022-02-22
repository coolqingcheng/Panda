using System.ComponentModel.DataAnnotations;

namespace Panda.Models;

public class CommentModels
{
}

public class CommentRequest
{
    [Required(ErrorMessage = "PostId不能为空")]
    public int PostId { get; set; }

    [Required(ErrorMessage = "评论信息不能为空")] public string Message { get; set; }

    public int CommentId { get; set; }
}