using Microsoft.EntityFrameworkCore;
using Panda.Admin;
using Panda.App;
using Panda.Site.Configs;
using Panda.Entity;
using Panda.Site.Admin;
using Panda.Tools;
using Panda.Tools.QueueTask;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Configuration.AddIniFile($"Site.{env.EnvironmentName}.ini", false, true);
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddRazorPages().AddMvcOptions(opt => { opt.Filters.Add<StatisticFilter>(); });


var services = builder.Services;

services.AddHttpClient();
services.AddDbContextPool<PandaContext>(
    opt =>
    {
        var db = builder.Configuration.GetConnectionString("MYSQL");
        opt.UseLazyLoadingProxies()
            .UseMySql(db, ServerVersion.AutoDetect(db))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
);

services.AddConfig(builder.Configuration);

services.AddQueueTask();

services.AddEasyCaching(options =>
{
    //use memory cache that named default
    options.UseInMemory(opt =>
    {
        opt.DBConfig.SizeLimit = 2000;
        opt.CacheNulls = true;
    });
});

services.AddTools();
services.AddAutoInject(opt =>
{
    opt.Options.Add(new AutoInjectOptionItem
    {
        EndWdith = "Service"
    });
    opt.Options.Add(new AutoInjectOptionItem
    {
        EndWdith = "Repository",
        InjectSelf = true
    });
});

builder.AddAdmin<PandaContext>();
services.AddSiteAdmin();
builder.AddAopCache();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.UseStatusCodePagesWithReExecute("/{0}.html");
app.MapRazorPages();

app.Run();