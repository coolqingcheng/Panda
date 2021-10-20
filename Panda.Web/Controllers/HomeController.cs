using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.UnitOfWork;
using Panda.Services.Account;
using Panda.Services.Posts;
using Panda.Web.Models;

namespace Panda.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IAccountService _accountService;

    private readonly IPostService _postService;


    public HomeController(ILogger<HomeController> logger, IAccountService accountService,
        IPostService postService)
    {
        _logger = logger;
        _accountService = accountService;
        _postService = postService;
    }

    /// <summary>
    /// 主页
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [HttpGet("/"), HttpGet("/page/{index:int}")]
    public async Task<IActionResult> Index(int index = 1)
    {
        var res = await _postService.GetArticleList(new PostRequest()
        {
            Index = index, Size = 10
        });
        ViewData["res"] = res;
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}