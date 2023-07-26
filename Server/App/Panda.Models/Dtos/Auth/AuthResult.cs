namespace Panda.Models.Dtos.Auth;

public class AuthResult<T> : BaseAuthResult where T : new()
{


    public T Data { get; set; }

    public AuthResult(bool isOk, string message = "") : base(message, isOk)
    {
        IsOk = isOk;
        Message = message;
    }
}

public class BaseAuthResult
{
    public string Message { get; set; }

    public bool IsOk { get; set; }

    public BaseAuthResult(string message, bool isOk)
    {
        Message = message;
        IsOk = isOk;
    }
}