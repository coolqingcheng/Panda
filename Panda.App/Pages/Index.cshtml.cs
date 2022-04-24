using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models;
using Panda.Entity.DataModels;
using Panda.Tools.Extensions;

namespace Panda.App.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly DbContext _dbContext;

    public IndexModel(ILogger<IndexModel> logger, DbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// 总条数
    /// </summary>
    public int Count { get; set; }


    /// <summary>
    /// 当前页
    /// </summary>
    public int CurrIndex { get; set; }

    public List<PostListItem> ListItems { get; set; }

    public async Task OnGet(int index = 1)
    {
        if (index < 1) index = 0;
        var query = _dbContext.Set<Posts>().Where(a => a.Status == PostStatus.Publish);

        Count = 50; //await query.CountAsync();

        ListItems = await query.Include(a => a.Account).OrderByDescending(a => a.UpdateTime).Page(index, 10).Select(a =>
            new PostListItem()
            {
                Link = a.CustomLink,
                Title = a.Title,
                UpdateTime = a.UpdateTime,
                Author = a.Account.NickName,
                Cover = a.Cover,
                Summay = a.Summary
            }).ToListAsync();
        CurrIndex = index;
    }
}