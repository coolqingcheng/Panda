namespace Panda.Tools.Exception;

public class UserException : System.Exception
{
    /// <summary>
    /// 友好错误信息
    /// </summary>
    public string Message { get; set; }

    public UserException(string message)
    {
        Message = message;
    }
}