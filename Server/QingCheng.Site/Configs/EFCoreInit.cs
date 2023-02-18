using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace QingCheng.Site.Configs;

public static class EFCoreInit
{
    public static void AddEntityFrameworkCore(this IServiceCollection services, Action<DbContextOptionsBuilder> opt)
    {
        services.AddDbContext<QingChengContext>(opt);
        services.AddScoped<DbContext>((provider) =>
        {
            return provider.GetRequiredService<QingChengContext>();
        });
    }
}