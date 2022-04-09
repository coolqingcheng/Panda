namespace Panda.Entity.Responses;

public class LoginStatusResult
{
    /// <summary>
    /// 是否初始化Admin账号
    /// </summary>
    public bool IsInit { get; set; }
    
    /// <summary>
    /// 当前是否已经登陆
    /// </summary>
    public bool IsLogin { get; set; }
}