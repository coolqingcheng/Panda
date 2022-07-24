using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Tools.Web.RazorPages;
using System.Threading.Channels;

namespace Panda.Site.Worker
{

    public class VisitStatisticBgWroker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly ILogger _logger;

        public VisitStatisticBgWroker(IServiceScopeFactory serviceScopeFactory,
            ILogger<VisitStatisticBgWroker> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        private bool running = true;

        public async Task DoWork()
        {

            var channelList = StatisticsRequestQueue.Instance.Reader.ReadAllAsync();
            await foreach (var item in channelList)
            {
                try
                {
                    var model = new AccessStatistic
                    {
                        UId = item.UId,
                        UA = item.UA,
                        OS = item.Info.OS.ToString(),
                        Browser = item.Info.UA.ToString(),
                        IP = item.Ip,
                        Referer = item.Referer,
                        Url = item.Url
                    };
                    using var scope = _serviceScopeFactory.CreateAsyncScope();
                    var db = scope.ServiceProvider.GetService<DbContext>();
                    await db!.Set<AccessStatistic>().AddRangeAsync(model);
                    _logger.LogInformation("写入访问数据:" + model.IP + "URL:" + model.Url);
                    await db!.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "写入访问日志错误");
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {

            running = false;
            StatisticsRequestQueue.Instance.Writer.Complete();
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            running = true;
            _ = DoWork();
            return Task.CompletedTask;
        }
    }

    public class StatisticsRequestQueue
    {
        static Channel<StatisticModel> channel = Channel.CreateBounded<StatisticModel>(200);

        public static Channel<StatisticModel> Instance => channel;
    }
}
