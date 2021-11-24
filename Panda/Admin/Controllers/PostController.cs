using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Services.Posts;

namespace Panda.Admin.Controllers;

public class PostController : AdminBaseController
{
    private readonly IPostService _articleService;

    public PostController(IPostService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost]
    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
        await _articleService.AddOrUpdate(request);
    }

    [HttpGet]
    public async Task<PostDetailItem> Get(int id)
    {
        return await _articleService.AdminGetPost(id);
    }

    [HttpGet]
    public async Task<PageDto<AdminPostItemResponse>> GetList([FromQuery]AdminPostGetListRequest request)
    {
       var res =   await _articleService.AdminGetList(request);
       return res;
    }
}