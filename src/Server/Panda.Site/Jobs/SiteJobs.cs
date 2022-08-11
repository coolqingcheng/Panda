namespace Panda.Site.Jobs
{
    using Hangfire;
    using TimeZoneConverter;

    public class SiteJobs
    {
        public static readonly TimeZoneInfo Tzi = TZConvert.GetTimeZoneInfo("Asia/Shanghai");
        public static void Init()
        {
            RecurringJob.AddOrUpdate<PostIndexCreator>(
                a => a.Exec(), Cron.Minutely(), Tzi);
        }
    }
}
