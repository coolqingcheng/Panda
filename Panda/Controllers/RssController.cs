using Microsoft.AspNetCore.Mvc;
using Panda.Services.DicData;
using Panda.Services.Posts;
using Panda.Tools.Web.RSS;

namespace Panda.Controllers;

public class RssController : Controller
{
    private readonly IPostService _postService;

    private readonly IDicDataService _dicDataService;

    public RssController(IPostService postService, IDicDataService dicDataService)
    {
        _postService = postService;
        _dicDataService = dicDataService;
    }

    [HttpGet("/feed")]
    public async Task<IActionResult> Index()
    {
        var list = await _postService.GetLatestPosts(100);
        var host = await _dicDataService.GetItemByCache("site", "host");
        var siteName = await _dicDataService.GetItemByCache("site", "site_name");
        var model = new RssModel()
        {
            Title = siteName?.Value,
            Link = host?.Value,
            Description = "我的博客"
        };
        foreach (var item in list)
        {
            model.Item.Add(new RssItem()
            {
                Title = item.Title,
                Link = $"{host}/post/{item.Id}.html",
                Description = item.Summary
            });
        }
        return Content(RssUtils.ToRssXml(model), "text/xml");
    }
}