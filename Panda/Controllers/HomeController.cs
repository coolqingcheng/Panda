using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.UnitOfWork;
using Panda.Services.Account;
using Panda.Services.Posts;
using Panda.Models;
using Panda.Services.DicData;

namespace Panda.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IAccountService _accountService;

    private readonly IPostService _postService;

    private readonly IDicDataService _dicDataService;


    public HomeController(ILogger<HomeController> logger, IAccountService accountService,
        IPostService postService, IDicDataService dicDataService)
    {
        _logger = logger;
        _accountService = accountService;
        _postService = postService;
        _dicDataService = dicDataService;
    }

    /// <summary>
    /// 主页
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [HttpGet("/"), HttpGet("/page/{index:int}")]
    public async Task<IActionResult> Index(int index = 1)
    {
        _logger.LogInformation("Index Log");
        var res = await _postService.GetPostList(new PostRequest()
        {
            Index = index, Size = 10
        });
        ViewData["res"] = res;
        ViewData["index"] = index;
        ViewData["description"] = await _dicDataService.GetItemByCache("site:site_description");
        return View();
    }

    /// <summary>
    /// 模板页面
    /// </summary>
    /// <param name="tmp"></param>
    /// <returns></returns>
    [HttpGet("/{tmp}.html")]
    public IActionResult TmpHtml(string tmp)
    {
        ViewData["tmp"] = tmp;
        return View();
    }

    [HttpGet("/error.html")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    /// <summary>
    /// 后台Spa页面
    /// </summary>
    /// <param name="env"></param>
    /// <returns></returns>
    [HttpGet("/admin")]
    [AllowAnonymous]
    public ActionResult AdminHtml([FromServices] IWebHostEnvironment env)
    {
        var path = Path.Combine(env.WebRootPath, "admin", "index.html");
        if (System.IO.File.Exists(path) == false)
        {
            return Content("后台文件找不到，请确定已经发布");
        }

        return PhysicalFile(path, "text/html; charset=utf-8");
    }

    /// <summary>
    /// 联系我
    /// </summary>
    /// <returns></returns>
    [HttpGet("/contact.html")]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpGet("/toUrl")]
    public IActionResult ToUrl(string url)
    {
        ViewData["Url"] = url;
        return View();
    }

    [HttpGet("/404.html")]
    public IActionResult Page404()
    {
        HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return View();
    }
}