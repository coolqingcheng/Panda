using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class FriendlyLinks:PandaBaseTable
{
    [Required]
    public string SiteName { get; set; }

    [Required]
    public string SiteUrl { get; set; }

    [Required]
    public int Weight { get; set; }

    public List<FriendlyLinkRecord> Records { get; set; }
}

public class FriendlyLinkRecord:PandaBaseTable
{
    public FriendlyLinks Links { get; set; }
    
    [Required]
    public string IP { get; set; }

    [Required]
    public string UA { get; set; }
}