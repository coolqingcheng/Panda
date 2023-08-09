SiteApp.Run(args,
    (services, config) =>
    {
        services.AddMyCaptcha(config);
        var connString = config.GetConnectionString("mysql");
        Console.WriteLine("读取Mysql连接:" + connString);
        services.AddEFMySql<PandaDbContext>(connString!);
    });