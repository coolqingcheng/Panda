using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models;
using Panda.Entity.DataModels;

namespace Panda.App.Pages.Tags;

public class Tags : PageModel
{
    public string TagName { get; set; }

    public int Index { get; set; }

    public int Count { get; set; }

    public List<PostListItem> ListItems { get; set; } = new();

    private readonly DbContext _dbContext;

    public Tags(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> OnGet(int id, int index)
    {
        if (index < 1) index = 1;
        Index = index;
        var tagItem = await _dbContext.Set<PostTags>().Where(a => a.Id == id).FirstOrDefaultAsync();
        if (tagItem == null)
        {
            return NotFound();
        }

        var query = _dbContext.Set<Posts>().Where(a => a.TagsRelations.Any(a => a.Tags == tagItem));
        ListItems = await query.OrderByDescending(a => a.AddTime).Select(a => new PostListItem()
        {
            Link = a.CustomLink,
            Title = a.Title,
            Author = a.Account.NickName,
            Summay = a.Summary,
            UpdateTime = a.UpdateTime
        }).ToListAsync();
        Count = await query.CountAsync();
        return Page();
    }
}