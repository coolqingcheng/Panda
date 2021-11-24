using Microsoft.AspNetCore.Mvc;
using Panda.Services.Posts;

namespace Panda.Controllers;

public class PostController : Controller
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    // GET
    [HttpGet("/post/{id:int}.html")]
    public async Task<IActionResult> Index(int id)
    {
        var res =  await _postService.GetPost(id);
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