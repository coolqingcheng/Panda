using Panda.Site.Jobs;

namespace Panda.Site.Extensions
{
    public static class SiteLuceneExtensions
    {
        public static IServiceCollection AddSiteLuceneIndex(this IServiceCollection services)
        {
            services.AddSingleton<PostLuceneIndex>();
            return services;
        }
    }
}
