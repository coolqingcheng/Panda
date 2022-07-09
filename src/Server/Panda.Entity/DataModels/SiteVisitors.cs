using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Models;

namespace Panda.Entity.DataModels;

/// <summary>
/// 临时访客
/// </summary>
public class SiteVisitors : BaseTable
{

    public virtual Accounts Account { get; set; }

    [StringLength(10)] public string? Code { get; set; }

    /// <summary>
    /// 随机密钥
    /// </summary>
    [StringLength(32)]
    public string ValidKey { get; set; }

    /// <summary>
    /// 发送验证码时间
    /// </summary>
    public DateTime? SendTime { get; set; }

    /// <summary>
    /// 验证码过期时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}