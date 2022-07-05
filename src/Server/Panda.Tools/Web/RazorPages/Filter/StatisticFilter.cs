using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Panda.Tools.Extensions;
using Panda.Tools.QueueTask;
using Panda.Tools.Web.RazorPages;
using UAParser;

namespace Panda.Tools.Web;

public class StatisticFilter : IAsyncPageFilter
{
    private readonly QueueTaskManager _queueTaskManager;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IWebHostEnvironment _hostEnvironment;

    private readonly IStatisticUtils _statisticUtils;

    public StatisticFilter(QueueTaskManager queueTaskManager, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment, IStatisticUtils statisticUtils)
    {
        _queueTaskManager = queueTaskManager;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
        _statisticUtils = statisticUtils;
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        if (_hostEnvironment.IsDevelopment())
        {
            return Task.CompletedTask;
        }
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
        _queueTaskManager.Run(async serviceProvider =>
        {
            var parser = Parser.GetDefault().Parse(ua);
            await _statisticUtils.Save(new StatisticModel()
            {
                Ip = ip,
                UA = ua,
                UId = uid,
                Url = url,
                Info = parser,
                Referer = referer,
            });

        });
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
        PageHandlerExecutionDelegate next)
    {
        await next.Invoke();
    }
}