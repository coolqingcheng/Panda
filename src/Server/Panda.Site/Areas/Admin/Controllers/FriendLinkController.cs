using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Services.FriendlyLink;
using Panda.Tools.Attributes;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

[PermissionGroup("博客-友情链接")]
public class FriendLinkController : AdminController
{
    private readonly IFriendlyLinkService _friendlyLinkService;

    public FriendLinkController(IFriendlyLinkService friendlyLinkService)
    {
        _friendlyLinkService = friendlyLinkService;
    }
    [Permission("查看")]
    [HttpGet]
    public async Task<PageDto<FriendlyLinkResponse>> GetList([FromQuery] FriendlyLinkRequest request)
    {
        return await _friendlyLinkService.AdminGetList(request);
    }
    [Permission("审核")]
    [HttpPost]
    public async Task Audit(FriendLinkAuditRequest request)
    {
        await _friendlyLinkService.Audit(request.Id, request.Status);
    }

    [Permission("删除")]
    [HttpDelete]
    public async Task Delete(int id)
    {
        await _friendlyLinkService.Delete(id);
    }
    [Permission("添加和编辑")]
    [HttpPost]
    public async Task AddOrUpdate(AddFriendLinkRequest request)
    {
        await _friendlyLinkService.AddOrUpdateFriendLink(request);
    }
    [Permission("查看")]
    [HttpGet]
    public async Task<FriendlyLinkResponse?> Get(int id)
    {
        return await _friendlyLinkService.Get(id);
    }
}