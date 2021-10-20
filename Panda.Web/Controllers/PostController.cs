using Microsoft.AspNetCore.Mvc;
using Panda.Services.Posts;

namespace Panda.Web.Controllers;

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
        var res =  await _postService.GetArticle(id);
        return View(res);
    }
}