using PandaTools.EFCore;

namespace PandaTools.Auth.Models;

public class BaseAccount : BaseEntity<Guid>
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Pwd { get; set; }

    /// <summary>
    ///     锁定时间
    /// </summary>
    public DateTime LockedTime { get; set; }

    /// <summary>
    ///     登录失败次数
    /// </summary>
    public int LoginFailCount { get; set; }


    public DateTime? UpdateTime { get; set; }
}