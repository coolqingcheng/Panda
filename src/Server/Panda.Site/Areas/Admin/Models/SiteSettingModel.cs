using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Panda.Site.Areas.Admin.Services.SiteOption;
using Panda.Tools.Attributes.Setting;

namespace Panda.Site.Areas.Admin.Models;

[SettingPrefix(Prefix = "Site")]
public class SiteSettingModel
{
    [Required]
    [Description("站点名称")]
    public string SiteName { get; set; }

    [Description("站点说明")]
    public string? SiteDesc { get; set; }

    [Description("ICP备案号")]
    public string? IcpNo { get; set; }

    [Description("第三方统计代码")]
    public string? StatisticCode { get; set; }
}

[SettingPrefix(Prefix = "Email")]
public class EmailSettingModel
{
    [Required]
    [Description("昵称")]
    public string NickName { get; set; }
    [Required]
    [Description("用户名")]
    public string UserName { get; set; }

    [Required]
    [Description("密码")]
    public string Password { get; set; }

    [Required]
    [Description("STMP地址")]
    public string Host { get; set; }

    [Required]
    [Description("端口")]
    public int Port { get; set; }

    [Required]
    [Description("使用SSL")]
    public bool UseSSL { get; set; }
}