

using Panda.Models.Data;

SiteApp.Run(args, (services, config) =>
{
    services.AddEFMySql<QingChengContext>(config.GetConnectionString("mysql")!);
});
