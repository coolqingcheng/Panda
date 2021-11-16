using Microsoft.AspNetCore.Mvc;
using Panda.Tools.Web.RSS;

namespace Panda.Controllers;

public class RssController : Controller
{
    [HttpGet("/feed")]
    public IActionResult Index()
    {
        var model = new RssModel()
        {
            Title = "Panda.blog",
            Link = "https://www.baidu.com",
            Description = "我的博客"
        };
        for (var i = 0; i < 10; i++)
        {
            model.Item.Add(new RssItem()
            {
                Title = "test-title-"+i,
                Link = $"https://www.baidu.com/{i}.html",
                Description = "无法傻屌发烧发烧发烧发烧发烧:"+i
            });
        }
        return Content(RssUtils.ToRssXml(model), "text/xml");
    }
}