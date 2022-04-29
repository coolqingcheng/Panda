using Microsoft.AspNetCore.Mvc.Filters;
using Panda.Tools.QueueTask;

namespace Panda.App;

public class StatisticFilter : IAsyncPageFilter
{
    private readonly QueueTaskManager _queueTaskManager;

    public StatisticFilter(QueueTaskManager queueTaskManager)
    {
        _queueTaskManager = queueTaskManager;
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        _queueTaskManager.Run(serviceProvider =>
        {
            
        });
        return  Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        await next.Invoke();
    }
}