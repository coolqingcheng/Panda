namespace Panda.Tools.Auth.Response;

public class AccountResp
{
    public Guid Id { get; set; }

    /// <summary>
    ///     用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///     邮箱
    /// </summary>
    public string Email { get; set; }

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

    /// <summary>
    /// 状态
    /// </summary>
    public AccountStatus Status
    {
        get
        {
            if (IsDisable)
            {
                return AccountStatus.Disabled;
            }

            if (LockedTime > DateTime.Now)
            {
                return AccountStatus.Locked;
            }

            return AccountStatus.Normal;
        }
    }
}

public enum AccountStatus
{
    Normal = 0,

    Disabled = 1,

    Locked = 2
}