using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PandaSite.Models;
using PandaSite.Services.Blogs;
using PandaTools.MiddleWare;

namespace PandaSite.Pages.Post;

public class Post : PageModel
{
    readonly PostService _postService;

    public Post(PostService postService)
    {
        _postService = postService;
    }

    public PostDetailModel Item { get; set; }

    public async Task<IActionResult> OnGet(int Id)
    {
        Console.WriteLine("Id" + Id);
        var item = await _postService.Get(Id);
        if (item == null)
        {
            return NotFound();
        }
        Item = item;
        await _postService.Visit(item.Id, HttpContext.GetClientIP(), HttpContext.Request.Headers.UserAgent, HttpContext.GetSiteUid());
        return Page();
    }
}