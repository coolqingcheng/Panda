using Panda.Entity.Responses;
using Panda.Models;

namespace Panda.Service.Comment;

public interface ICommentService
{
    Task Submit(CommentRequest request);

    Task<PageDto<CommentItem>> GetComments(GetCommentRequest request);
}