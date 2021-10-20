using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Services.Posts;

namespace Panda.Web.Admin.Controllers;

public class ArticleController : AdminBaseController
{
    private readonly IPostService _articleService;

    public ArticleController(IPostService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost]
    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
        await _articleService.AddOrUpdate(request);
    }

    [HttpGet]
    public async Task<ArticleDetailItem> Get(int id)
    {
        return await _articleService.GetArticle(id);
    }

    [HttpGet]
    public async Task<PageResponse<AdminArticleItemResponse>> GetList([FromQuery]AdminPostGetListRequest request)
    {
       var res =   await _articleService.AdminGetList(request);
       return res;
    }
}