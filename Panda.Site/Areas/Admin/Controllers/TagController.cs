using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.PostTag;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

public class TagController : AdminController
{
    private readonly IPostTagService _tagService;

    public TagController(IPostTagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<PageDto<TagResponse>> GetList([FromQuery] TagRequest request)
    {
        return await _tagService.GetList(request);
    }

    [HttpGet]
    public async Task<List<TagResponse>> SearchTag(string key)
    {
        return await _tagService.SearchTag(key);
    }

    [HttpPost]
    public async Task AddOrUpdate(TagAddRequest request)
    {
        await _tagService.AddOrUpdate(request);
    }
}