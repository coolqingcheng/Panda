using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Panda.Tools.QueueTask;

public class QueueTaskManager : IDisposable
{
    private static ConcurrentQueue<Action<IServiceProvider>> Queue { get; set; } = new();

    private static bool _isRun = true;

    public static void Run(Action<IServiceProvider> action)
    {
        Queue.Enqueue(action);
    }


    public QueueTaskManager(IServiceProvider serviceProvider)
    {
        _ = Task.Run(async () =>
        {
            while (_isRun)
            {
                if (Queue.TryDequeue(out var action) == false)
                {
                    await Task.Delay(100);
                }

                action?.Invoke(serviceProvider);
            }
        });
    }

    public void Dispose()
    {
        // todo 现在直接扔掉，不处理队列的数据
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