using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Responses;
using Panda.Site.Models;
using Panda.Site.Services.Site;
using Panda.Tools.Auth.Models;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;
using Panda.Tools.Web;
using UAParser;

namespace Panda.Site.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommentController : Controller
{
    private readonly DbContext _context;

    private readonly IVisitorService _visitor;

    public CommentController(DbContext context, IVisitorService visitor)
    {
        _context = context;
        _visitor = visitor;
    }

    /// <summary>
    /// 提交评论
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task Submit(CommentSubmitModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Code) == false && string.IsNullOrWhiteSpace(model.Email) == false)
        {
            await _visitor.ConfirmEmail(model.Email, model.Code);
        }
        else
        {
            await _visitor.CheckVisitorIdentity();
            model.Email = App.GetCookie(IVisitorService.VisitorCookieNickEmail)!;
        }

        //markdown转html

        var html = Markdown.ToHtml(model.Content);
        model.Content = html;
        // 过滤xss

        model.Content = model.Content.HtmlXssFilter();

        var account = await _context.Set<Accounts>()
            .Where(a => a.Email == model.Email).FirstOrDefaultAsync();
        var post = await _context.Set<Posts>().Where(a => a.CustomLink == model.PostLink).FirstOrDefaultAsync();
        if (post == null)
        {
            throw new UserException("评论主体不存在");
        }

        await _context.Set<PostComments>().AddAsync(new PostComments()
        {
            Content = model.Content,
            Account = account!,
            AddTime = DateTime.Now, Post = post, Ip = Request.HttpContext.GetClientIp(),
            UserAgent = Request.HttpContext.GetUserAgent()
        });
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// 获取评论
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<PageDto<CommentModel>> GetList([FromQuery] QueryComment model)
    {
        var query = () => _context.Set<PostComments>().Where(a => a.Post.CustomLink == model.Link).AsQueryable();

        var list = await query().OrderByDescending(a => a.AddTime).Page(model.Index, model.Size).Select(a =>
            new CommentModel()
            {
                Id = a.Id,
                Content = a.Content,
                NickName = a.Account.NickName,
                AddTime = a.AddTime,
                UA = a.UserAgent
            }).ToListAsync();

        var parser = Parser.GetDefault();
        foreach (var item in from item in list let ua = parser.Parse(item.UA) select item)
        {
            item.Os = item.Os;
            item.Browser = item.Browser;
        }

        return new PageDto<CommentModel>()
        {
            Total = await query().CountAsync(),
            Data = list
        };
    }
}