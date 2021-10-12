using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.UnitOfWork;
using Panda.Tools;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionStrings:mysql").Value;


builder.Services.AddDbContext<PandaContext>(
    opt => { opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); }
);

builder.Services.AddControllersWithViews();

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