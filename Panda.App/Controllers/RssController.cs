using Microsoft.AspNetCore.Mvc;
using Panda.App.Services.Post;
using Panda.Tools.Web.RSS;

namespace Panda.App.Controllers;

public class RssController : Controller
{
    private readonly IPostService _postService;

    public RssController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("/feed")]
    public async Task<IActionResult> Index()
    {
        var model = await _postService.GetRssData(100);
        return Content(RssUtils.ToRssXml(model), "text/xml");
    }
}