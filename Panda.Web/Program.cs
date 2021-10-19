using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.UnitOfWork;
using Panda.Tools;
using Panda.Tools.Filter;
using Panda.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionStrings:mysql").Value;


builder.Services.AddDbContext<PandaContext>(
    opt => { opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); }
);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


builder.Services.AddControllersWithViews(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
    opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    opt.Filters.Add<CSRFFilter>();
});
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


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();