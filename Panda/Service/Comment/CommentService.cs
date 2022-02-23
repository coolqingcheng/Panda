using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Responses;
using Panda.Entity.UnitOfWork;
using Panda.Models;
using Panda.Repository.Post;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;

namespace Panda.Service.Comment;

public class CommentService : ICommentService
{
    private readonly PostRepository _postRepository;

    private readonly PostCommentRepository _commentRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUnitOfWork _unitOfWork;

    public CommentService(PostRepository postRepository, PostCommentRepository commentRepository,
        IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task Submit(CommentRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        var post = await _postRepository.FindById(request.PostId);
        if (post.AllowComment == false)
        {
            throw new UserException("当前评论未开启");
        }

        var userAgent = _httpContextAccessor.HttpContext!.Request.Headers.UserAgent.ToString();

        var comment = await _commentRepository.Where(a => a.Id == request.CommentId).FirstOrDefaultAsync();

        post.Comments.Add(new PostComments()
        {
            Content = request.Message,
            AnswerComment = comment,
            UserAgent = userAgent,
            Ip = _httpContextAccessor.GetClientIP()
        });
        await _unitOfWork.CommitAsync();
    }

    public async Task<PageDto<CommentItem>> GetComments(GetCommentRequest request)
    {
        var query = _commentRepository.Where(a => a.Post.Id == request.PostId && a.ParentComment == null);
        var commentList = await query.Select(a => new CommentItem()
        {
            Id = a.Id,
            Content = a.Content,
            AddTime = a.AddTime,
            NickName = a.Account.UserName,
            AnswerId = a.AnswerComment.Id
        }).Page(request).ToListAsync();
        return new PageDto<CommentItem>()
        {
            Data = commentList,
            Total = await query.CountAsync()
        };
    }
}