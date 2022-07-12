using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.Build.Framework;

namespace Panda.Site.Models;

public class CommentModel
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public string Content { get; set; }
    public DateTime AddTime { get; set; }
    public string Os { get; set; }
    public string Browser { get; set; }

    [JsonIgnore] public string UA { get; set; }
}

public class CommentSubmitModel
{
    public string Content { get; set; }

    public string Email { get; set; }

    public string Code { get; set; }

    public string PostLink { get; set; }
}

public class CommentSubmitModelValidator : AbstractValidator<CommentSubmitModel>
{
    public CommentSubmitModelValidator()
    {
        RuleFor(a => a.Content).NotEmpty().WithMessage("评论内容不能为空");
        RuleFor(a => a.PostLink).NotEmpty().WithMessage("找不到评论主体");
    }
}

public class QueryComment
{
    public string Link { get; set; }
    public int Index { get; set; }
}