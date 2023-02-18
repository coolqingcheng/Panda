using System;
using Microsoft.AspNetCore.Http;

namespace System;

public class UserException : Exception
{
    public int Code { get; set; }

    public UserException(string message, int code = StatusCodes.Status500InternalServerError) : base(message)
    {
        Code = code;
    }
}