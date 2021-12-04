using Panda.Entity.DataModels;

namespace Panda.Entity.Responses;

public class FriendlyLinkResponse
{
    public int Id { get; set; }

    public string SiteName { get; set; }

    public string SiteUrl { get; set; }

    public int Weight { get; set; }

    public DateTime AddTime { get; set; }

    public AuditStatusEnum AuditStatus { get; set; }
}