var app = SiteApp.Run(args,
    (services, config) => { services.AddMyCaptcha(config); });
app.AddPgSql<PandaDbContext>();