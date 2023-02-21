

using QingCheng.Site.Configs;
using QingCheng.Tools.App;

SiteApp.Run(args, (services,config) =>
{
    services.AddEntityFrameworkCore((opt) =>
    {
        var connString = config.GetConnectionString("mysql");
        opt.UseMySql(connString, ServerVersion.AutoDetect(connString));
    });
});
