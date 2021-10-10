using Autofac.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Autofac;
using Panda.Tools;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionStrings:mysql").Value;




var fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.MySql,
        connectionString)
#if DEBUG
    .UseAutoSyncStructure(true) //自动迁移实体的结构到数据库 
#endif
    .Build();
builder.Services.AddSingleton(fsql);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoInject(opt =>
{
    opt.AssemblyStringList.TryAdd("Panda.Services", "Service");
    opt.AssemblyStringList.TryAdd("Panda.Repositorys", "Repository");
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();