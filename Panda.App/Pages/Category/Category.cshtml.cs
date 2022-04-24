using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models;
using Panda.Entity.DataModels;
using Panda.Tools.Extensions;

namespace Panda.App.Pages.Category;

public class Category : PageModel
{
    public Category(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string CateName { get; set; }


    private readonly DbContext _dbContext;

    public int Count { get; set; }

    public int Index { get; set; }


    public List<PostListItem> ListItems { get; set; }

    public async Task<IActionResult> OnGet(int id, int index = 1)
    {
        var query = _dbContext.Set<Posts>()
            .Where(a => a.ArticleCategoryRelations.Any(b => b.Categories.Id == id));
        ListItems = await query.OrderByDescending(a => a.AddTime).Page(index, 10).Select(a => new PostListItem()
        {
            Link = a.CustomLink,
            Cover = a.Cover,
            Title = a.Title,
            Author = a.Account.NickName,
            Summay = a.Summary,
            UpdateTime = a.UpdateTime
        }).ToListAsync();
        if (ListItems.Count == 0)
        {
            return NotFound();
        }

        Count = await query.CountAsync();
        return Page();
    }
}