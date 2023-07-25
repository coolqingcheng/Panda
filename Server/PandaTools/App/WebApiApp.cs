using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PandaTools.Serilog;
using PandaTools.Auth;
using PandaTools.Configs;
using PandaTools.Exceptions;

namespace PandaTools.App;

/// <summary>
/// webapi项目启动模板
/// </summary>
public static class WebApiApp
{
    /// <summary>
    /// 启动一个WebApi的程序
    /// 1. 注入了日志库
    /// 2. 配置了webapi的json响应 日期格式化
    /// 3. jwt认证配置
    /// 4. 异常拦截
    /// 5. swagger配置
    /// 6. 基础的库配置
    /// </summary>
    /// <param name="args"></param>
    /// <param name="action"></param>
    /// <param name="appAction"></param>
    public static void Run(string[] args,Action<IServiceCollection,IConfiguration>? action = null,Action<WebApplication>? appAction = null)
    {
        var logger = SerilogExtensions.Instance();
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddLog();
            var services = builder.Services;
            services.AddWebApiConfig();
            services.AddDistributedMemoryCache();
            builder.AddApplication();
            services.AddJwtAuth(builder.Configuration);
            action?.Invoke(services, builder.Configuration);
            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseEx();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            appAction?.Invoke(app);
            app.Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, $"{nameof(WebApiApp)}:程序已经停止");
            throw;
        }
    }
}