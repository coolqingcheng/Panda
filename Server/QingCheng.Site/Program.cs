using Microsoft.EntityFrameworkCore;
using QingCheng.Tools;
using QingCheng.Tools.Serilog;
using QingCheng.Site.Services;
using QingCheng.Site.Data;
using QingCheng.Site.Configs;
using QingCheng.WebApi;
using QingCheng.Site;
using QingCheng.Tools.Auth;
using Serilog;
using QingCheng.Tools.Exceptions;
using Microsoft.AspNetCore.Rewrite;
using QingCheng.Tools.MiddleWare;

var logger = SerilogExtensions.Instance();
try
{

 

    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseSerilog();

    var services = builder.Services;

    services.AddEntityFrameworkCore((opt) =>
    {
        var connString = builder.Configuration.GetConnectionString("mysql");
        opt.UseMySql(connString, ServerVersion.AutoDetect(connString)).UseSnakeCaseNamingConvention();
    });
    services.AddApiConfig();
    services.AddSite();
    services.AddDistributedMemoryCache();

    builder.AddApp();
    builder.Services.AddCookieAuth();

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
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "≥Ã–Ú“—æ≠Õ£÷π");
    throw;
}