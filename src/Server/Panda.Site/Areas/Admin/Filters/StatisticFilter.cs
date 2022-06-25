﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Panda.Entity.DataModels;
using Panda.Tools.Extensions;
using Panda.Tools.QueueTask;
using UAParser;

namespace Panda.App;

public class StatisticFilter : IAsyncPageFilter
{
    private readonly QueueTaskManager _queueTaskManager;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IWebHostEnvironment _hostEnvironment;

    public StatisticFilter(QueueTaskManager queueTaskManager, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
    {
        _queueTaskManager = queueTaskManager;
        _httpContextAccessor = httpContextAccessor;
        _hostEnvironment = hostEnvironment;
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
            var item = new AccessStatistic
            {
                UId = uid,
                UA = ua,
                OS = parser.OS.ToString(),
                Browser = parser.UA.ToString(),
                IP = ip,
                Referer = referer,
                Url = url
            };
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetService<DbContext>();
            db!.Set<AccessStatistic>().Add(item);
            var count = await db.SaveChangesAsync();
        });
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
        PageHandlerExecutionDelegate next)
    {
        await next.Invoke();
    }
}