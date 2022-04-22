using COSXML.Model.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Panda.App.Models;
using Panda.Entity.DataModels;

namespace Panda.App.Pages;

public class Post : PageModel
{
    public Post(DbContext db)
    {
        _db = db;
    }

    public PostModel PostModel { get; set; }


    private readonly DbContext _db;

    public async Task<IActionResult> OnGetAsync(string link)
    {
        var postItem = await _db.Set<Posts>().Where(a => a.CustomLink == link).Select(a => new PostModel()
        {
        }).AsNoTracking().FirstOrDefaultAsync();
        if (postItem == null)
        {
            return NotFound();
        }

        return Page();
    }
}