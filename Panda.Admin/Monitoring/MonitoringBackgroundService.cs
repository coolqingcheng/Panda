using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Panda.Admin.DataModels;
using UAParser;

namespace Panda.Admin.Monitoring;

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
            if (list.Any())
            {
                foreach (var model in list)
                {
                    var ua = Parser.GetDefault().Parse(model.UA);
                    var item = await _context.Set<Monitorings>()
                        .FirstOrDefaultAsync(a => a.Url == model.Url, cancellationToken: stoppingToken);
                    if (item != null)
                    {
                        item.MonitoringDetails.Add(new MonitoringDetail()
                        {
                            Ip = model.Ip,
                            Os = ua.OS.ToString(),
                            Browser = ua.UA.ToString(),
                            UA = model.UA ?? ""
                        });
                    }
                    else
                    {
                        await _context.Set<Monitorings>().AddAsync(new Monitorings()
                        {
                            Url = model.Url,
                            MonitoringDetails = new List<MonitoringDetail>()
                            {
                                new()
                                {
                                    Ip = model.Ip,
                                    Os = ua.OS.ToString(),
                                    Browser = ua.UA.ToString(),
                                    UA = model.UA ?? ""
                                }
                            }
                        }, stoppingToken);
                    }
                }

                await _context.SaveChangesAsync(stoppingToken);
            }
            else
            {
                await Task.Delay(1000 * 5, stoppingToken);
            }
        }
    }
}