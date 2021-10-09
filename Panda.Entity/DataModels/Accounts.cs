using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Accounts:PandaBaseTable
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Passwd { get; set; }
}