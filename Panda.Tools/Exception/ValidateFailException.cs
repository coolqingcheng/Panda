namespace Panda.Tools.Exception;

public class ValidateFailException : System.Exception
{
    public ValidateFailException(string message) : base(message)
    {
    }
}