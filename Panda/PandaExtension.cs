using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Services.DicData;
using Panda.Tools;
using Panda.Tools.Web;

namespace Panda
{
    public static class PandaExtension
    {
        public static void AddPanda(this WebApplicationBuilder app)
        {
           
            var services = app.Services;
           
            services.AddHttpClient();
            services.AddDbContextPool<PandaContext>(
                opt =>
                {
                    var db = app.Configuration.GetConnectionString("MYSQL_DB");
                    opt.UseMySql(db, ServerVersion.AutoDetect(db))
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            );
            services.AddEasyCaching(options =>
            {
                //use memory cache that named default
                options.UseInMemory(opt =>
                {
                    opt.DBConfig.SizeLimit = 2000;
                    opt.CacheNulls = true;
                });
            });
           
            services.AddTools();
            services.AddAutoInject(opt =>
            {
                opt.Options.Add(new AutoInjectOptionItem()
                {
                    EndWdith = "Service"
                });
                opt.Options.Add(new AutoInjectOptionItem()
                {
                    EndWdith = "Repository",
                    InjectSelf = true
                });
            });
            CacheKeys.ValidateKeyRepetition();
        }
    }
}