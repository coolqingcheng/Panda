using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Responses;
using Panda.Site.Models;
using Panda.Tools.Extensions;
using UAParser;

namespace Panda.Site.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CommentController : Controller
{
    private readonly DbContext _context;

    public CommentController(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 获取评论
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<PageDto<CommentModel>> GetList([FromQuery]QueryComment model)
    {
        var query = () => _context.Set<PostComments>().Where(a => a.Post.CustomLink == model.Link).AsQueryable();

        var list = await query().OrderByDescending(a => a.AddTime).Page(model.Index, 20).Select(a => new CommentModel()
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