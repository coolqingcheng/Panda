using System.Net;

namespace Panda.Tools.Exception;

public class UserException : System.Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public UserException(string? message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        this.StatusCode = statusCode;
    }
}