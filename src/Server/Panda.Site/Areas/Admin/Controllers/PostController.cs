using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.Posts;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

public class PostController : AdminController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
        await _postService.AddOrUpdate(request);
    }

    [HttpDelete]
    public async Task Delete(int id)
    {
        await _postService.Delete(id);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<PostDetailItem> Get(int id)
    {
        return await _postService.AdminGetPost(id);
    }

    [HttpGet]
    public async Task<PageDto<AdminPostItemResponse>> GetList([FromQuery] AdminPostGetListRequest request)
    {
        var res = await _postService.AdminGetList(request);
        return res;
    }
}