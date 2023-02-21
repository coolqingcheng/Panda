

using QingCheng.Site;
using QingCheng.Tools.App;
using QingCheng.Toos.EFCore;

SiteApp.Run(args, (services, config) =>
{
    services.AddEFMySql<QingChengContext>(config.GetConnectionString("mysql")!);
});
