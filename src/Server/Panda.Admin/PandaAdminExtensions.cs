using System.Net;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Panda.Admin.Controllers;
using Panda.Admin.Repositorys;
using Panda.Admin.Repositorys.DicData;
using Panda.Admin.Services.Account;
using Panda.Admin.Services.Email;
using Panda.Admin.Services.Statistics;
using Panda.Tools;
using Panda.Tools.Email;
using Panda.Tools.Filter;
using Panda.Tools.NLog;
using Panda.Tools.Web;
using Panda.Tools.Web.RazorPages;

namespace Panda.Admin;

public static class PandaAdminExtensions
{
    public static void AddAdmin<T>(this WebApplicationBuilder app) where T : DbContext
    {
        app.AddNLog();
        var services = app.Services;
        services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
        services.AddScoped<AccountRepository>();
        services.AddScoped<DicDataRepository>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<DbContext>(a => a.GetService<T>()!);
        services.AddScoped<IStatisticUtils, StatisticUtils>();
        services.AddScoped<IEmailSender, SiteOptionEmailSender>();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
        services.AddScanFluentValidation();
        services.AddControllers()
            .AddFluentValidation()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
            options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errorMsg = context.ModelState.Values.SelectMany(a => a.Errors).Select(a => a.ErrorMessage).FirstOrDefault();
                var result = new JsonResult(new { Message = errorMsg });
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            };
        }); ;
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "ºóÌ¨apiÎÄµµ",
                Version = "v1",
                Description = ""
            });
            var list = new string[] { "Panda.Site.xml", "Panda.Admin.xml" };
            foreach (var item in list)
            {
                var file = Path.Combine(AppContext.BaseDirectory, item);
                var path = Path.Combine(AppContext.BaseDirectory, file);
                if (File.Exists(path) == false)
                {
                    continue;
                }

                c.IncludeXmlComments(path, true);
                c.OrderActionsBy(o => o.RelativePath);
            }
        });
        // Add services to the container.
        services.AddRazorPages()
            .AddMvcOptions(opt => { opt.Filters.Add<StatisticFilter>(); });
        services.AddAutoInject(opt =>
        {
            opt.Options.Add(new AutoInjectOptionItem
            {
                EndWdith = "Service"
            });
        });
    }


}