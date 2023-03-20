﻿using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QingCheng.Tools.EFCore;

namespace QingCheng.Toos.EFCore;

public static class EFExtensions
{
    /// <summary>
    /// 添加EFDbContext
    /// </summary>
    /// <typeparam name="TDB"></typeparam>
    /// <param name="services"></param>
    /// <param name="opt"></param>
    public static void AddEFCore<TDB>(this IServiceCollection services, Action<DbContextOptionsBuilder> opt) where TDB:DbContext
    {
        
        services.AddScoped<TranContext>();
        services.AddDbContext<TDB>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            opt(options);
        });
        services.AddScoped<DbContext>(a =>
        {
            return a.GetService<TDB>()!;
        });
        
    }

    /// <summary>
    /// 添加Mysql
    /// </summary>
    /// <typeparam name="DB"></typeparam>
    /// <param name="services"></param>
    /// <param name="connString"></param>
    public static void AddEFMySql<DB>(this IServiceCollection services,string connString) where DB : DbContext
    {
        services.AddEFCore<DB>(options =>
        {
            options.UseMySql(connString, ServerVersion.AutoDetect(connString));
        });
    }
}