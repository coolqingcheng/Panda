using Microsoft.AspNetCore.Mvc.Filters;

namespace Panda.Tools.Filter;

public class ExceptionFilterAttribute:IExceptionFilter
{
    public void OnException(ExceptionContext context)
    { 
        
    }
}