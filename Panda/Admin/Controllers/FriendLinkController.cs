using Microsoft.AspNetCore.Mvc;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Services.FriendlyLink;

namespace Panda.Admin.Controllers;

public class FriendLinkController : AdminController
{
    private readonly IFriendlyLinkService _friendlyLinkService;

    public FriendLinkController(IFriendlyLinkService friendlyLinkService)
    {
        _friendlyLinkService = friendlyLinkService;
    }

    [HttpGet]
    public async Task<PageDto<FriendlyLinkResponse>> GetList([FromQuery] FriendlyLinkRequest request)
    {
        return await _friendlyLinkService.AdminGetList(request);
    }

    [HttpPost]
    public async Task Audit(FriendLinkAuditRequest request)
    {
        await _friendlyLinkService.Audit(request.Id, request.Status);
    }

    [HttpDelete]
    public async Task Delete(int id)
    {
        await _friendlyLinkService.Delete(id);
    }

    [HttpPost]
    public async Task AddOrUpdate(AddFriendLinkRequest request)
    {
        await _friendlyLinkService.AddOrUpdateFriendLink(request);
    }

    [HttpGet]
    public async Task<FriendlyLinkResponse?> Get(int id)
    {
        return await _friendlyLinkService.Get(id);
    }
}