using Microsoft.Extensions.DependencyInjection;

namespace Panda.Admin.Monitoring;

public static class MonitoringExtension
{
    public static void AddMonitoring(this IServiceCollection service)
    {
        service.AddSingleton<IMonitoringService, MonitoringService>();
        service.AddHostedService<MonitoringBackgroundService>();
    }
}