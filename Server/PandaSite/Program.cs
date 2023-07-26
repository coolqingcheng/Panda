SiteApp.Run(args,
    (services, config) => { services.AddEFMySql<PandaDbContext>(config.GetConnectionString("mysql")!); });