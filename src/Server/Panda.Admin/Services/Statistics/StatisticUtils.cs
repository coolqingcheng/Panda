using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Panda.Entity.DataModels;
using Panda.Tools.Web.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace Panda.Admin.Services.Statistics
{
    public class StatisticUtils : IStatisticUtils
    {
        private readonly DbContext _db;

        public StatisticUtils(DbContext db)
        {
            _db = db;
        }

        public async Task Save(StatisticModel model)
        {
            var item = new AccessStatistic
            {
                UId = model.UId,
                UA = model.UA,
                OS = model.Info.OS.ToString(),
                Browser = model.Info.UA.ToString(),
                IP = model.Ip,
                Referer = model.Referer,
                Url = model.Url
            };
            _db!.Set<AccessStatistic>().Add(item);
            var count = await _db.SaveChangesAsync();
        }
    }
}
