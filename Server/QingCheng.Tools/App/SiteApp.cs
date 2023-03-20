﻿using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QingCheng.Site.Configs;
using QingCheng.Tools.Auth;
using QingCheng.Tools.Exceptions;
using QingCheng.Tools.MiddleWare;
using QingCheng.Tools.Serilog;

namespace QingCheng.Tools.App;

public class SiteApp
{
    public static void Run(string[] args,Action<IServiceCollection,IConfiguration>? action,Action<WebApplication>? appAction = null)
    {
        var logger = SerilogExtensions.Instance();
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddLog();
            var services = builder.Services;
        
            
            services.InjectServices(Assembly.GetCallingAssembly());
           
            services.AddWebApiConfig();
                services.AddRazorPages();
            services.AddDistributedMemoryCache();
            builder.AddApplication();
            builder.Services.AddCookieAuth();
            action?.Invoke(services,builder.Configuration);
            var app = builder.Build();
        
        
        
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                var option = new RewriteOptions();
                option.AddRedirectToNonWwwPermanent();
                option.AddRedirectToHttps();
                app.UseRewriter(option);
            }
            app.UseStaticFiles();
            app.UseEx();
            app.UseSiteUid();
            app.UseCookieAuth();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();
            appAction?.Invoke(app);
            app.Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "程序已经停止");
            throw;
        }
    }
}