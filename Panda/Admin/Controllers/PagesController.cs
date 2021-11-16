using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Services.Pages;

namespace Panda.Admin.Controllers;

public class PagesController : AdminBaseController
{
    private readonly IPageService _pageService;

    public PagesController(IPageService pageService)
    {
        _pageService = pageService;
    }

    [HttpPost]
    public async Task AddOrUpdate(PagesRequest request)
    {
        await _pageService.AddOrUpdate(request);
    }

    [HttpGet]
    public async Task<PageDto<PagesItem>> GetList([FromQuery]GetPagesRequest request)
    {
        return await _pageService.GetPagesList(request);
    }
}