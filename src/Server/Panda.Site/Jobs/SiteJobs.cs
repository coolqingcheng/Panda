namespace Panda.Site.Jobs
{
    using Hangfire;
    using TimeZoneConverter;

    public class SiteJobs
    {
        public static readonly TimeZoneInfo Tzi = TZConvert.GetTimeZoneInfo("Asia/Shanghai");
        public static void Init()
        {
            //一个小时更新一下索引
            RecurringJob.AddOrUpdate<PostIndexCreator>(
                a => a.Exec(), Cron.Hourly(), Tzi,"创建全文索引");
        }
    }
}
