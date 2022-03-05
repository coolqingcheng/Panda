using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using Panda;
using global::Panda.Tools.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.AddPanda();

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
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
//}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();