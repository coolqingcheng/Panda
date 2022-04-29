using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Panda.Tools.QueueTask;

public class QueueTaskManager : IDisposable
{
    private ConcurrentQueue<Func<IServiceProvider, Task>> Queue { get; set; } = new();

    private bool _isRun = true;

    private ILogger _logger;

    public void Run(Func<IServiceProvider, Task> action)
    {
        Queue.Enqueue(action);
    }


    public QueueTaskManager(IServiceProvider serviceProvider, ILogger<QueueTaskManager> logger)
    {
        _logger = logger;
        _ = Task.Run(async () =>
        {
            while (_isRun)
            {
                if (Queue.TryDequeue(out var action) == false)
                {
                    await Task.Delay(1000);
                    continue;
                }

                Console.WriteLine("执行统计" + DateTime.Now);

                try
                {
                    await action?.Invoke(serviceProvider)!;
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e);
                    _logger.LogError(e.Message,e);
                }
            }
        });
    }

    public void Dispose()
    {
        // todo 现在直接扔掉，不处理队列的数据
        Queue.Clear();
        _isRun = false;
    }
}

public static class QueueTaskManagerExtension
{
    public static IServiceCollection AddQueueTask(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<QueueTaskManager>();
        return serviceCollection;
    }
}