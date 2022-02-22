using Panda.Models;

namespace Panda.Service.Comment;

public interface ICommentService
{
    Task Submit(CommentRequest request);
}