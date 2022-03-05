using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.UnitOfWork;
using Panda.Filters;
using Panda.Services.DicData;
using Panda.Tools;
using Panda.Tools.Filter;
using Panda.Tools.Web;
using System.Net;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Panda.Repository.Post;
using Panda.Tools.NLog;

namespace Panda
{
    public static class PandaExtension
    {
        public static void AddPanda(this WebApplicationBuilder app)
        {
            app.AddNLog();
            var services = app.Services;
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            services.AddHttpClient();
            services.AddDbContextPool<PandaContext>(
                opt =>
                {
                    var db = app.Configuration.GetConnectionString("MYSQL_DB");
                    opt.UseMySql(db, ServerVersion.AutoDetect(db))
                        .EnableSensitiveDataLogging()
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
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.SelectMany(a => a.Errors)
                        .Select(a => new { a.ErrorMessage });
                    string errMsg = string.Join("|", errors);
                    var result = new JsonResult(new { Message = errMsg });
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return result;
                };
            });
            ;
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
            CacheKeys.ValidateKeyRepetition();
        }
    }
}