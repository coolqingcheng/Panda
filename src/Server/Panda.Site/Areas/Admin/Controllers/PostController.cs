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


[PermissionGroup("����-���¹���")]
public class PostController : AdminController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [Permission("��Ӻͱ༭")]
    [HttpPost]
    public async Task AddOrUpdate(PostAddOrUpdate request)
    {
        await _postService.AddOrUpdate(request);
    }

    [Permission("ɾ��")]
    [HttpDelete]
    public async Task Delete(int id)
    {
        await _postService.Delete(id);
    }

    [Permission("�鿴")]
    [HttpGet]
    public async Task<PostDetailItem> Get(int id)
    {
        return await _postService.AdminGetPost(id);
    }

    [Permission("�鿴")]
    [HttpGet]
    public async Task<PageDto<AdminPostItemResponse>> GetList([FromQuery] AdminPostGetListRequest request)
    {
        var res = await _postService.AdminGetList(request);
        return res;
    }
}