using Microsoft.AspNetCore.Mvc;
using Panda.Services.Posts;
using Panda.SiteMap;

namespace Panda.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    // GET
    [HttpGet("/post/{link}.html")]
    public async Task<IActionResult> Index(string link)
    {
        var res =  await _postService.GetPostByLink(link);
        return View(res);
    }

    [HttpGet("/search")]
    public async Task<IActionResult> Search(string keyword)
    {
        var res = await _postService.Search(keyword);
        ViewData["res"] = res;
        ViewData["keyword"] = keyword;
        return View();
    }
}