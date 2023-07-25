using Microsoft.AspNetCore.Http;

namespace PandaTools.Exceptions;

public class UserException : Exception
{
    public int Code { get; set; }

    public UserException(string message, int code = StatusCodes.Status500InternalServerError) : base(message)
    {
        Code = code;
    }
}