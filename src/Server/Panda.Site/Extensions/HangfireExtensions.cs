using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Panda.Site.Jobs;
using Panda.Tools.Extensions;

namespace Panda.Site.Extensions
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddHangFireEx(this IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });
            services.AddScoped<PostIndexCreator>();
            services.AddHangfireServer();
            return services;
        }

        public static void UseHangFireEx(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuth() }
            });
            SiteJobs.Init();
        }
    }


    public class HangFireAuth : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

#if DEBUG
            return true;
#endif
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.IsAdmin();
        }
    }
}
