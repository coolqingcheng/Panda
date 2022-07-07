using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.Posts;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Controllers;
using Panda.Tools.Auth.Permission;

namespace Panda.Site.Areas.Admin.Controllers;


[PermissionGroup("博客-文章管理")]
public class PostController : AdminController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [Permission("添加和编辑")]
    [HttpPost]
    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
        await _postService.AddOrUpdate(request);
    }

    [Permission("删除")]
    [HttpDelete]
    public async Task Delete(int id)
    {
        await _postService.Delete(id);
    }

    [Permission("查看")]
    [HttpGet]
    public async Task<PostDetailItem> Get(int id)
    {
        return await _postService.AdminGetPost(id);
    }

    [Permission("查看")]
    [HttpGet]
    public async Task<PageDto<AdminPostItemResponse>> GetList([FromQuery] AdminPostGetListRequest request)
    {
        var res = await _postService.AdminGetList(request);
        return res;
    }
}