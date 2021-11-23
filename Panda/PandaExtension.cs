﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.UnitOfWork;
using Panda.Filters;
using Panda.Services.DicData;
using Panda.Tools;
using Panda.Tools.Filter;
using Panda.Tools.Web;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Panda
{
    public static class PandaExtension
    {
        public static void AddPanda(this IServiceCollection services)
        {

            var db = Environment.GetEnvironmentVariable("MYSQL_DB");
            if (string.IsNullOrWhiteSpace(db))
            {
                Console.WriteLine("mysql连接没有配置");
                return;
            }

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            services.AddDbContextPool<PandaContext>(
                opt =>
                {
                    opt.UseMySql(db, ServerVersion.AutoDetect(db)).EnableSensitiveDataLogging()
                            .EnableDetailedErrors();
                }
            );
            services.AddEasyCaching(options =>
            {
                //use memory cache that named default
                options.UseInMemory(opt =>
                {
                    opt.DBConfig.SizeLimit = 2000;
                    opt.CacheNulls = true;
                });
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add<GlobalExceptionFilter>();
                opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                opt.Filters.Add<CSRFFilter>();
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
            });
            services.AddTools();
            services.AddScoped<IDicDataProvider, EFDicDataProvider>();

            services.AddAntiforgery(options =>
                options.HeaderName = "X-CSRF-TOKEN"
            );


            services.AddAutoInject(opt =>
            {
                opt.Options.Add(new AutoInjectOptionItem()
                {
                    EndWdith = "Service"
                });
                opt.Options.Add(new AutoInjectOptionItem()
                {
                    EndWdith = "Repository",
                    InjectSelf = true
                });
            });
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

        }
    }
}