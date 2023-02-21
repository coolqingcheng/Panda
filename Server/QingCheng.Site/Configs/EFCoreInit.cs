using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QingCheng.Tools.EFCore;

namespace QingCheng.Site.Configs;

public static class EFCoreInit
{
    public static void AddEntityFrameworkCore(this IServiceCollection services, Action<DbContextOptionsBuilder> opt)
    {
        
        services.AddScoped<TranContext>();
        services.AddDbContext<QingChengContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            opt(options);
        });
        services.AddScoped<DbContext>((provider) =>
        {
            return provider.GetRequiredService<QingChengContext>();
        });
    }
}