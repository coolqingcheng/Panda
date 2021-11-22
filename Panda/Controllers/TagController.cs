using Microsoft.AspNetCore.Mvc;
using Panda.Services.Posts;
using Panda.Services.Tags;

namespace Panda.Controllers;

public class TagController : Controller
{
    private readonly IPostTagService _postTagService;

    private readonly IPostService _postService;

    public TagController(IPostTagService postTagService, IPostService postService)
    {
        _postTagService = postTagService;
        _postService = postService;
    }

    [HttpGet("/tag/{id:int}")]
    [HttpGet("/tag/{id:int}_{index:int}")]
    public async Task<IActionResult> Index(int id, int index)
    {
        var tag = await _postTagService.GetId(id);
        var list = await _postService.GetPostListByTagId(id);
        ViewData["list"] = list;
        ViewData["tag"] = tag.TagName;
        ViewData["id"] = tag.Id;
        ViewData["index"] = index;
        return View();
    }
}