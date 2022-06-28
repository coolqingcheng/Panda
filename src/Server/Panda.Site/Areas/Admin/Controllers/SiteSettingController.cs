using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Site.Areas.Admin.Models;
using Panda.Site.Areas.Admin.Services.SiteOption;
using Panda.Tools.Auth.Controllers;

namespace Panda.Site.Areas.Admin.Controllers;

/// <summary>
/// 网站设置
/// </summary>
public class SiteSettingController : AdminController
{
    private readonly ISiteOptionService _siteOptionService;

    public SiteSettingController(ISiteOptionService siteOptionService)
    {
        _siteOptionService = siteOptionService;
    }

    [HttpPost]
    public async Task SetSiteInfo(SiteSettingModel model)
    {
        var dic = _siteOptionService.GetDic(model);
        await _siteOptionService.AddOrUpdate(dic);
    }

    [HttpGet]
    public async Task<SiteSettingModel> GetSiteInfo()
    {
        return await _siteOptionService.GetModel<SiteSettingModel>();
    }
}