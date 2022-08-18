using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Models;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.Posts;
using Panda.Site.Jobs;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Controllers;
using Panda.Tools.Auth.Permission;

namespace Panda.Site.Areas.Admin.Controllers;


[PermissionGroup("博客-文章管理")]
public class PostController : AdminController
{
    private readonly IPostService _postService;

    private readonly PostLuceneIndex _postLucene;

    public PostController(IPostService postService, PostLuceneIndex postLucene)
    {
        _postService = postService;
        _postLucene = postLucene;
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
    [Permission("删除全文索引")]
    [HttpDelete]
    public void DeleteFullIndex()
    {
        _postLucene.DeleteAll();

    }
}