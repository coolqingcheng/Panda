using System.Collections.Concurrent;
using System.Linq;

namespace Panda.Admin.Monitoring;

public interface IMonitoringService
{
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task SaveAsync(MonitoringModel model);

    /// <summary>
    /// 获取队列数据
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    IEnumerable<MonitoringModel> Take(int count);
}

public class MonitoringService : IMonitoringService
{
    private readonly ConcurrentQueue<MonitoringModel> _queue = new();

    public Task SaveAsync(MonitoringModel model)
    {
        _queue.Enqueue(model);
        return Task.CompletedTask;
    }

    public IEnumerable<MonitoringModel> Take(int count)
    {
        for (var i = 0; i < count; i++)
        {
            if (_queue.TryDequeue(out var model))
            {
                yield return model;
            }
        }
    }
}

public class MonitoringModel
{
    public string Ip { get; set; }

    public string? UA { get; set; }

    public string Url { get; set; }

    public DateTime CreateTime { get; set; }
}