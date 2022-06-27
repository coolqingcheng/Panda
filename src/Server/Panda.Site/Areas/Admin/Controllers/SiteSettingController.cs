using Microsoft.AspNetCore.Mvc;
using Panda.Site.Areas.Admin.Models;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

/// <summary>
/// 网站设置
/// </summary>
public class SiteSettingController : AdminController
{
    [HttpPost]
    public Task SetSiteInfo(SiteSettingRequest request)
    {
        return Task.CompletedTask;
    }
}