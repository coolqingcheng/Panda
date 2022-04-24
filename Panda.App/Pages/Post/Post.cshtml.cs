using COSXML.Model.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models;
using Panda.Entity.DataModels;

namespace Panda.App.Pages.Post;

public class Post : PageModel
{
    public Post(DbContext db)
    {
        _db = db;
    }

    public PostModel? PostModel { get; set; }


    private readonly DbContext _db;

    public async Task<IActionResult> OnGetAsync(string link)
    {
        PostModel = await _db.Set<Posts>().Where(a => a.CustomLink == link)
            .Select(a => new PostModel()
            {
                Title = a.Title,
                Categorys = a.ArticleCategoryRelations.Select(b => new PostCategory()
                {
                    CateName = b.Categories.CategoryName,
                    CateId = b.Categories.Id
                }).ToList(),
                Content = a.Content,
                UpdateTime = a.UpdateTime,
                Author = a.Account.NickName,
                Link = a.CustomLink
            }).AsNoTracking().FirstOrDefaultAsync();
        if (PostModel == null)
        {
            return NotFound();
        }

        return Page();
    }
}