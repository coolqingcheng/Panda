using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Admin.Attributes;
using Panda.Entity.DataModels;
using Panda.SiteStatistic.Models;
using Panda.Tools.Auth.Controllers;
using Panda.Tools.Extensions;

namespace Panda.SiteStatistic.Controllers;

[Permission("网站访问数据统计")]
public class SiteStatisticController : AdminController
{
    private readonly DbContext _dbContext;

    // GET
    public SiteStatisticController(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// 获取昨日汇总统计
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<SiteStatisticModel> GetStatisticCollect([FromQuery] SiteStatisticRequest request)
    {
        request.BuildDate();
        var query = _dbContext.Set<AccessStatistic>().
            WhereIf(request.Begin!=null,a => a.AddTime >= request.Begin && a.AddTime < request.End);
        return new SiteStatisticModel()
        {
            Ip = await query.Select(a=>a.IP).Distinct().CountAsync(),
            Pv = await query.CountAsync(),
            Uv = await query.Select(a => a.UId).Distinct().CountAsync()
        };
    }
}