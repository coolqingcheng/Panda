using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Models;
using Panda.Tools.Auth.Controllers;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using Panda.Tools.Filter;
using Panda.Tools.Helper;
using Panda.Tools.Web;

namespace Panda.Site.Areas.Admin.Controllers;

public class CommentsController : AdminController
{
    private readonly DbContext _context;

    private readonly IpHelper _ipHelper;


    public CommentsController(DbContext context, IpHelper ipHelper)
    {
        _context = context;
        _ipHelper = ipHelper;
    }

    /// <summary>
    /// PostId获取评论
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageDto<SiteCommentModel>> GetCommentByPostId(int postId)
    {
        var query = _context.Set<PostComments>().Where(a => a.Post.Id == postId);

        var list = await query.OrderByDescending(a => a.AddTime).Select(a => new SiteCommentModel()
        {
            Id = a.Id,
            Content = a.Content,
            AddTime = a.AddTime,
            Email = a.Account.Email,
            Ip = a.Ip,
            NickName = a.Account.NickName,
            UA = a.UserAgent
        }).ToListAsync();

        list.ForEach(a => a.Region = _ipHelper.GetRegion(a.Ip));

        return new PageDto<SiteCommentModel>()
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    /// <summary>
    /// 回复评论
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [EFTransacation]
    public async Task AskComment(AskCommentRequest request)
    {
        var comment = await _context.Set<PostComments>().Where(a => a.Id == request.CommentId).FirstOrDefaultAsync();
        if (comment == null)
        {
            throw new UserException("评论不存在");
        }

        var account = await App.GetAccount();

        await _context.Set<PostComments>().AddAsync(new PostComments()
        {
            Content = request.Content,
            AddTime = DateTime.Now,
            Account = account,
            Ip = Request.HttpContext.GetClientIp(),
            UserAgent = HttpContext.GetUserAgent(),
            ReplyId = comment.Id,
            CommentReads = new List<PostCommentRead>()
            {
                new()
                {
                    Accounts = account
                }
            }
        });
        await _context.SaveChangesAsync();
    }
}