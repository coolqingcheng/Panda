SiteApp.Run(args,
    (services, config) =>
    {
        services.AddMyCaptcha(config);
        services.AddEFMySql<PandaDbContext>(config.GetConnectionString("mysql")!);
    });