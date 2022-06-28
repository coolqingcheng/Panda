using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Panda.Site.Areas.Admin.Services.SiteOption;

namespace Panda.Site.Areas.Admin.Models;

public class SiteSettingModel
{
    [Required] public string SiteName { get; set; }

    [Required] public string SiteDesc { get; set; }

    [Required] public string SiteHost { get; set; }

    public string IcpNo { get; set; }

    public string StatisticCode { get; set; }
}