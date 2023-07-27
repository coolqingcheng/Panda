using System.ComponentModel.DataAnnotations;

namespace Panda.Services.Config;

public class SiteConfig
{
    [Required] public string SiteName { get; set; }

    [Required] public string ICP { get; set; }

    /// <summary>
    ///     站点描述
    /// </summary>
    public string SiteDesc { get; set; }
}