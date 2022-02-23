using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Responses;
using Panda.Entity.UnitOfWork;
using Panda.Models;
using Panda.Repository.Account;
using Panda.Repository.Post;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using UAParser;

namespace Panda.Service.Comment;

public class CommentService : ICommentService
{
    private readonly PostRepository _postRepository;

    private readonly PostCommentRepository _commentRepository;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUnitOfWork _unitOfWork;

    private readonly AccountRepository _accountRepository;

    public CommentService(PostRepository postRepository, PostCommentRepository commentRepository,
        IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, AccountRepository accountRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
    }

    public async Task Submit(CommentRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        var post = await _postRepository.FindById(request.PostId);
        if (post.AllowComment == false)
        {
            throw new UserException("当前评论未开启");
        }

        var account = await _accountRepository.Where(a => a.Email == "qingchengcode@qq.com").FirstOrDefaultAsync();

        var userAgent = _httpContextAccessor.HttpContext!.Request.Headers.UserAgent.ToString();

        var comment = await _commentRepository.Where(a => a.Id == request.CommentId).FirstOrDefaultAsync();

        post.Comments.Add(new PostComments()
        {
            Account = account!,
            Content = request.Message,
            ReplyId = comment?.Id,
            UserAgent = userAgent,
            Ip = _httpContextAccessor.GetClientIP()
        });
        await _unitOfWork.CommitAsync();
    }

    public async Task<PageDto<CommentItem>> GetComments(GetCommentRequest request)
    {
        var query = _commentRepository.Where(a => a.Post.Id == request.PostId && a.Pid == 0);
        var commentList = await query.Page(request).Select(a => new CommentItem()
        {
            Id = a.Id,
            Content = a.Content,
            AddTime = a.AddTime,
            NickName = a.Account.UserName,
            ReplyId = a.ReplyId,
            UserAgent = a.UserAgent,
            Children = _commentRepository.Where(b => b.Pid == a.Id).Take(20).Select(b => new CommentItem()
            {
                Id = b.Id, Content = b.Content, NickName = b.Account.NickName,
                ReplyId = b.ReplyId, UserAgent = b.UserAgent
            }).ToList()
        }).ToListAsync();
        foreach (var item in commentList)
        {
            ParseUserAgent(item);
            foreach (var itemChild in item.Children)
            {
                ParseUserAgent(itemChild);
            }
        }

        return new PageDto<CommentItem>()
        {
            Data = commentList,
            Total = await query.CountAsync()
        };
    }

    private static void ParseUserAgent(CommentItem item)
    {
        var client = Parser.GetDefault().Parse(item.UserAgent);
        if (client != null)
        {
            item.Browser = $"{client.UA.Family}:{client.UA.Major}-{client.UA.Minor}";
            item.Os = $"{client.OS.Family}:{client.OS.Major}-{client.OS.Minor}";
            item.Device = client.Device.Family;
        }
    }
}