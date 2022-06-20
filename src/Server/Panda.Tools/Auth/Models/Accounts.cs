using System.ComponentModel.DataAnnotations;
using Panda.Admin.Entities.DataModels;

namespace Panda.Tools.Auth.Models;

public class Accounts : KeyGuidTable
{
    /// <summary>
    ///     用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     昵称
    /// </summary>
    [StringLength(50)]
    public string NickName { get; set; }

    /// <summary>
    ///     邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     密码
    /// </summary>
    public string Passwd { get; set; }

    /// <summary>
    ///     上次登录时间
    /// </summary>
    public DateTimeOffset LastLoginTime { get; set; }

    /// <summary>
    ///     锁定时间
    /// </summary>
    public DateTimeOffset LockedTime { get; set; }

    /// <summary>
    ///     登录失败次数
    /// </summary>
    public int LoginFailCount { get; set; }

    /// <summary>
    ///     是否禁用
    /// </summary>
    public bool IsDisable { get; set; }
}