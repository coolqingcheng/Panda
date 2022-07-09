using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Panda.Tools.Web;

namespace Panda.Tools.Filter;

/// <summary>
/// EF事务
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class EFTransacationAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbContext = App.GetService<DbContext>();
        var tran = await dbContext!.Database.BeginTransactionAsync();
        await next.Invoke();
        await tran.CommitAsync();
    }
}