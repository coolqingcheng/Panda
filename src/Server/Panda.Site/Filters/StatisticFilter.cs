using Microsoft.AspNetCore.Mvc.Filters;
using Panda.Site.Worker;
using Panda.Tools.Extensions;
using Panda.Tools.Web.RazorPages;
using UAParser;

namespace Panda.Site.Filter;

public class StatisticFilter : IAsyncPageFilter
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IWebHostEnvironment _hostEnvironment;

    private readonly IServiceProvider _serviceProvider;

    public StatisticFilter(
        IHttpContextAccessor httpContextAccessor,
        IWebHostEnvironment hostEnvironment,
        IServiceProvider serviceProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _serviceProvider = serviceProvider;
    }

    public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        //if (_hostEnvironment.IsDevelopment())
        //{
        //    return Task.CompletedTask;
        //}
        if (context.HttpContext.Request.Cookies.TryGetValue("id", out var uid) == false)
        {
            uid = Guid.NewGuid().ToString("N");
            context.HttpContext.Response.Cookies.Append("id", uid);
        }

        Console.WriteLine("数据统计开始");

        context.HttpContext.Request.Headers.TryGetValue("user-agent", out var ua);
        var ip = context.HttpContext.GetClientIp();
        context.HttpContext.Request.Headers.TryGetValue("referer", out var referer);
        string? url = context.HttpContext.Request.Path.Value;

        var parser = Parser.GetDefault().Parse(ua);
        var model = new StatisticModel()
        {
            Ip = ip,
            UA = ua,
            UId = uid,
            Url = url,
            Info = parser,
            Referer = referer,
        };
        await StatisticsRequestQueue.Instance.Writer.WriteAsync(model);
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
        PageHandlerExecutionDelegate next)
    {
        await next.Invoke();
    }
}