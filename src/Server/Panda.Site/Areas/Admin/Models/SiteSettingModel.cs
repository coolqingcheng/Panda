using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Panda.Site.Areas.Admin.Models;

public class SiteSettingModel
{
}

public class SiteSettingRequest
{
    [Description("网站名称"), Required] public string SiteName { get; set; }

    [Description("网站描述"), Required] public string SiteDesc { get; set; }

    [Description("网站域名"), Required] public string SiteHost { get; set; }

    [Description("Icp备案号")] public string IcpNo { get; set; }

    [Description("统计代码")] public string StatisticCode { get; set; }
}