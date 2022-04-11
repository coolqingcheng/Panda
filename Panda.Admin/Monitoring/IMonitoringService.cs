using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Panda.Admin.DataModels;

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

public class MonitoringBackgroundService : BackgroundService
{
    private readonly IMonitoringService _monitoringService;

    private readonly DbContext _context;

    public MonitoringBackgroundService(IMonitoringService monitoringService, DbContext context)
    {
        _monitoringService = monitoringService;
        _context = context;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var list = _monitoringService.Take(10);
            foreach (var model in list)
            {
                var item = await _context.Set<Monitorings>().FirstOrDefaultAsync(a => a.Url == model.Url);
                if (item != null)
                {
                }
                else
                {
                    await _context.Set<Monitorings>().AddAsync(new Monitorings()
                    {
                        Url = model.Url,
                        MonitoringDetails = new List<MonitoringDetail>()
                        {
                        }
                    });
                }
            }

            await _context.SaveChangesAsync();
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