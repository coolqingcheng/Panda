using System.Net;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Panda.Admin.Repositorys;
using Panda.Admin.Repositorys.DicData;
using Panda.Admin.Services.Account;
using Panda.Admin.Services.DicData;
using Panda.Tools.CSRF;
using Panda.Tools.EF.UnitOfWork;
using Panda.Tools.Filter;
using Panda.Tools.NLog;
using Panda.Tools.Web;

namespace Panda.Admin;

public static class PandaAdminExtensions
{
    public static void AddAdmin<T>(this WebApplicationBuilder app) where T : DbContext
    {
        app.AddNLog();
        var services = app.Services;
        services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
        services.AddScoped(typeof(AccountRepository<>));
        services.AddScoped(typeof(IAccountService<>),
            typeof(AccountService<>));
        services.AddScoped<IDicDataService, DicDataService>();
        services.AddScoped<DicDataRepository>();
        services.AddScoped<DbContext>(a => a.GetService<T>()!);

        services.AddAntiforgery(options =>
            options.HeaderName = "X-CSRF-TOKEN"
        );
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
            options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Values.SelectMany(a => a.Errors)
                    .Select(a => new {a.ErrorMessage});
                var errMsg = string.Join("|", errors);
                var result = new JsonResult(new {Message = errMsg});
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return result;
            };
        });

        services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        services.AddScoped<IDicDataProvider, EFDicDataProvider>();
    }
}