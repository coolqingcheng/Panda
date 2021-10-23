using System.Net;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.UnitOfWork;
using Panda.Tools;
using Panda.Tools.Filter;
using Panda.Tools.Web;
using Panda.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

var db =  Environment.GetEnvironmentVariable("MYSQL_DB");
if (string.IsNullOrWhiteSpace(db))
{
    Console.WriteLine("mysql连接没有配置");
    return;
}

builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
builder.Services.AddDbContext<PandaContext>(
    opt => { opt.UseMySql(db, ServerVersion.AutoDetect(db)); }
);
builder.Services.AddEasyCaching(options =>
{
    //use memory cache that named default
    options.UseInMemory("default");
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
    opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    opt.Filters.Add<CSRFFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
}).AddRazorRuntimeCompilation();
builder.Services.AddTools();

builder.Services.AddAntiforgery(options =>
    options.HeaderName = "X-CSRF-TOKEN"
);


builder.Services.AddAutoInject(opt =>
{
    opt.AssemblyStringList.Add(new AutoInjectOptionItem()
    {
        AssemblyName = "Panda.Services",
        EndWdith = "Service"
    });
    opt.AssemblyStringList.Add(new AutoInjectOptionItem()
    {
        AssemblyName = "Panda.Repositorys",
        EndWdith = "Repository",
        InjectSelf = true
    });
});
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseExceptionHandler(builder =>
    {
        builder.Run(async context =>
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
                context.Request.Query["X-Requested-With"] == "XMLHttpRequest")
            {
                await context.Response.WriteAsJsonAsync(new {message = "服务器繁忙"});
            }
            else
            {
                context.Response.Redirect("/Home/Error");
            }
        });
    });
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();