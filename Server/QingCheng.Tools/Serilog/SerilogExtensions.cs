using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace QingCheng.Tools.Serilog;

public static class SerilogExtensions
{
    public static ILogger Instance()
    {
        var path = Directory.GetCurrentDirectory();//GetDirectoryName((new SerilogExtensions()).GetType().Assembly?.Location);
        Console.WriteLine("获取当前路径:"+path);
        path = Path.Combine(path!, "Logs");
        var infoPath = Path.Combine(path, "Info", "info_.log");
        var errPath = Path.Combine(path, "Error", "err_.log");
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Logger(x =>
                x.Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Information).WriteTo.File(infoPath, rollingInterval: RollingInterval.Day)
            )
            .WriteTo.Logger(x =>
                x.Filter.ByIncludingOnly(a => a.Level == LogEventLevel.Error).WriteTo.File(errPath, rollingInterval: RollingInterval.Day)
            )
            .CreateLogger();
        return Log.Logger;
    }

    public static void AddLog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();
    }
}