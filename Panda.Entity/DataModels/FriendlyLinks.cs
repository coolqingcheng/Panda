using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class FriendlyLinks : PandaBaseTable
{
    [Required]
    public string SiteName { get; set; }

    [Required]
    public string SiteUrl { get; set; }

    [Required]
    public int Weight { get; set; }

    /// <summary>
    /// ÉóºË×´Ì¬
    /// </summary>
    public AuditStatusEnum AuditStatus { get; set; }

    public List<FriendlyLinkRecord> Records { get; set; }
}

public class FriendlyLinkRecord : PandaBaseTable
{
    public FriendlyLinks Links { get; set; }

    [Required]
    public string IP { get; set; }

    [Required]
    public string UA { get; set; }
}

public enum AuditStatusEnum
{
    [Description("´ýÉóºË")]
    unaudit = 0,
    [Description("Í¨¹ý")]
    Pass = 1,
    [Description("¾Ü¾ø")]
    Reject = 2
}