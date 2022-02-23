using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Accounts : KeyGuidTable
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [StringLength(50)]
    public string NickName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Passwd { get; set; }

    /// <summary>
    /// 上次登录时间
    /// </summary>
    public DateTime LastLoginTime { get; set; }

    /// <summary>
    /// 锁定时间
    /// </summary>
    public DateTime LockedTime { get; set; }

    /// <summary>
    /// 登录失败次数
    /// </summary>
    public int LoginFailCount { get; set; }
}