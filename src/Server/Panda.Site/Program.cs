using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Panda.Admin;
using Panda.Site.Configs;
using Panda.Entity;
using Panda.Tools;
using Panda.Tools.QueueTask;
using Panda.Tools.Exception;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
services.AddHttpClient();
services.AddDbContext<PandaContext>(
    opt =>
    {
        var envMysql = Environment.GetEnvironmentVariable("MYSQL", EnvironmentVariableTarget.Process);
        var db = string.IsNullOrWhiteSpace(envMysql) == false ? envMysql : builder.Configuration.GetConnectionString("MYSQL");
        opt.UseLazyLoadingProxies()
            .UseMySql(db, ServerVersion.AutoDetect(db))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            ;
    }
);

services.AddConfig(builder.Configuration);
services.AddQueueTask();
services.AddTools();
services.AddAutoInject(opt =>
{
    opt.Options.Add(new AutoInjectOptionItem
    {
        EndWdith = "Service"
    });
});


builder.AddAdmin<PandaContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMyEx();
app.UseStaticFiles();


app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
app.UseTools();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}");
});
app.UseStatusCodePagesWithReExecute("/{0}.html");
app.MapRazorPages();

app.Run();