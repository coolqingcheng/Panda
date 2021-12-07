using System.ComponentModel.DataAnnotations;
using System.Web;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Admin.Models;
using Panda.Tools.Exception;
using Panda.Tools.FileStorage;
using Panda.Tools.Security;
using Panda.Tools.Web;

namespace Panda.Admin.Controllers;

[AllowAnonymous]
public class SpiderController : AdminController
{
    private readonly IHttpClientFactory _clientFactory;

    private readonly IFileStorage _fileStorage;

    public SpiderController(IHttpClientFactory clientFactory, IFileStorage fileStorage)
    {
        _clientFactory = clientFactory;
        _fileStorage = fileStorage;
    }

    /// <summary>
    /// 转载cnBlogs.com的文章
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    [HttpGet()]
    public async Task<SpiderResult> GetCnBlog([Required] string url)
    {
        if (!url.StartsWith("https://www.cnblogs.com"))
        {
            throw new UserException("当前接口不支持非https:www.cnblogs.com的链接");
        }

        using var http = _clientFactory.CreateClient();
        var result = await http.GetStringAsync(url);
        var parser = new HtmlParser().ParseDocument(result);
        var body = parser.QuerySelector("#cnblogs_post_body");
        if (body == null)
        {
            throw new UserException("页面解析错误！");
        }

        var hElement = body.QuerySelectorAll("h1,h1,h3,h4,h5,h6,h7,a,p,img");

        foreach (var element in hElement)
        {
            element.RemoveAttribute("id");
            element.RemoveAttribute("class");
            element.RemoveAttribute("style");

            if (element.TagName.ToLower() == "a")
            {
                var href = element.GetAttribute("href");
                if (string.IsNullOrWhiteSpace(href) != false) continue;
                if (href.StartsWith("/"))
                {
                    element.SetAttribute("href", $"/toUrl?url={HttpUtility.UrlEncode(href)}");
                }
            }

            if (element.TagName.ToLower() == "img")
            {
                element.RemoveAttribute("alt");
                element.RemoveAttribute("loading");
                Console.WriteLine("抓图片:" + element.GetAttribute("src"));
                try
                {
                    var imgUrl = element.GetAttribute("src");
                    var response = await http.GetAsync(imgUrl);
                    if (!response.IsSuccessStatusCode) continue;
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    var ext = MimeUtils.GetMimeExtName(response.Content.Headers.ContentType?.MediaType);
                    var uploadResult =
                        await _fileStorage.SaveAsync(bytes, $"{Md5Helper.ComputeHash(bytes)[..10]}.{ext}");
                    if (uploadResult.Success)
                    {
                        element.SetAttribute("src", uploadResult.Url);
                    }
                }
                catch (Exception e)
                {
                    element.RemoveFromParent();
                }
            }
        }

        return new SpiderResult()
        {
            Html = body.InnerHtml
        };
    }
}