SiteApp.Run(args,
    (services, config) =>
    {
        services.AddMyCaptcha(config);
        services.AddPgSql<PandaDbContext>(config);
    });