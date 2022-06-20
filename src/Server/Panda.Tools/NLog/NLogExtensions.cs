using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Panda.Tools.NLog;

public static class NLogExtensions
{
    public static void AddNLog(this WebApplicationBuilder builder)
    {
        // builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Information);
        builder.Host.UseNLog();
    }
}