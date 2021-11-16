using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Services.Tags;

namespace Panda.Admin.Controllers;

public class TagController : AdminBaseController
{

    private readonly IPostTagService _tagService;

    public TagController(IPostTagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<PageDto<TagResponse>> GetList(TagRequest request)
    {
        return await _tagService.GetList(request);
    }
    [HttpGet]
    public async Task<List<TagResponse>> SearchTag(string key)
    {
        return await _tagService.SearchTag(key);
    }
}