using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.PostTag;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

[PermissionGroup("博客-标签管理")]
public class TagController : AdminController
{
    private readonly IPostTagService _tagService;

    public TagController(IPostTagService tagService)
    {
        _tagService = tagService;
    }
    [Permission("查看")]
    [HttpGet]
    public async Task<PageDto<TagResponse>> GetList([FromQuery] TagRequest request)
    {
        return await _tagService.GetList(request);
    }
    [Permission("查看")]
    [HttpGet]
    public async Task<List<TagResponse>> SearchTag(string key)
    {
        return await _tagService.SearchTag(key);
    }
    [Permission("添加和编辑")]
    [HttpPost]
    public async Task AddOrUpdate(TagAddRequest request)
    {
        await _tagService.AddOrUpdate(request);
    }
}