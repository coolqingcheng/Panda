using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Services.Account;
using Panda.Services.Article;
using Panda.Web.Models;

namespace Panda.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IAccountService _accountService;

    private readonly IArticleService _articleService;

    public HomeController(ILogger<HomeController> logger, IAccountService accountService,
        IArticleService articleService)
    {
        _logger = logger;
        _accountService = accountService;
        _articleService = articleService;
    }


    public async Task<IActionResult> Index()
    {
        await _articleService.GetArticleList(new ArticleRequest()
        {
            Index = 1, Size = 10
        });
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}