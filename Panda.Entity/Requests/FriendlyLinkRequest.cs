using System.ComponentModel.DataAnnotations;
using Panda.Entity.DataModels;
using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class FriendlyLinkRequest : BasePageRequest
{
}

public class AddFriendLinkRequest
{
    public int Id { get; set; }

    /// <summary>
    /// 站点名称
    /// </summary>
    [Required]
    public string siteName { get; set; }

    [Required] public string siteUrl { get; set; }

    public AuditStatusEnum? AuditStatus { get; set; }
}

public class FriendLinkAuditRequest
{
    [Required] public int Id { get; set; }

    [Required] public AuditStatusEnum Status { get; set; }
}