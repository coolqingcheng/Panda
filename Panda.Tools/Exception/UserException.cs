namespace Panda.Tools.Exception;

public class UserException : System.Exception
{
    public string Message { get; set; }

    public UserException(string message)
    {
        Message = message;
    }
}