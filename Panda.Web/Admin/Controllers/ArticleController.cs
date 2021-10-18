using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Services.Article;

namespace Panda.Web.Admin.Controllers;

public class ArticleController : AdminBaseController
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task AddOrUpdate(ArticleAddOrUpdate request)
    {
        await _articleService.AddOrUpdate(request);
    }

    [HttpGet]
    public async Task<ArticleDetailItem> Get(int id)
    {
        return await _articleService.GetArticle(id);
    }
}