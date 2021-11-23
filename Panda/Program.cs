using System.Net;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.UnitOfWork;
using Panda.Services.DicData;
using Panda.Tools;
using Panda.Tools.Filter;
using Panda.Tools.Web;
using Panda.Filters;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Panda;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPanda();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error.html");
}
else
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseExceptionHandler(builder =>
    {
        builder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
                context.Request.Query["X-Requested-With"] == "XMLHttpRequest")
            {
                await context.Response.WriteAsJsonAsync(new { message = "服务器繁忙" });
            }
            else
            {
                context.Response.Redirect("/error.html");
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