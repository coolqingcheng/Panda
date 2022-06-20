using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Responses;
using Panda.Site.Areas.Admin.Models;
using Panda.Tools.Auth.Controllers;
using Panda.Tools.Extensions;
using Panda.Tools.Helper;

namespace Panda.Site.Areas.Admin.Controllers;




/// <summary>
/// 仪表盘分析
/// </summary>
public class StatisticController : AdminController
{
    private readonly DbContext _dbContext;

    private readonly IpHelper _ipHelper;

    public StatisticController(DbContext dbContext, IpHelper ipHelper)
    {
        _dbContext = dbContext;
        _ipHelper = ipHelper;
    }

    /// <summary>
    /// 获取汇总信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<StatisticModel> Get()
    {
        var postCount = await _dbContext.Set<Posts>().CountAsync();
        var begin = DateTimeOffset.Now.Date;
        var end = DateTimeOffset.Now.AddDays(1).Date;
        var ipCount = await _dbContext.Set<AccessStatistic>()
            .Where(a => a.AddTime >= begin && a.AddTime < end).Select(a => a.IP).Distinct()
            .CountAsync();
        return new StatisticModel()
        {
            PostCount = postCount,
            IpCount = ipCount
        };
    }

   /// <summary>
   /// 获取访问记录
   /// </summary>
   /// <param name="page"></param>
   /// <param name="size"></param>
   /// <returns></returns>
    [HttpGet]
    public async Task<PageDto<RecentAccessHistory>> GetRecentAccessRecord(int page,int size)
    {
        var query = _dbContext.Set<AccessStatistic>().AsQueryable();

        var list = await query.OrderByDescending(a => a.AddTime).Page(page, size).ProjectToType<RecentAccessHistory>()
                .ToListAsync();

        list.ForEach(a => {
            a.Region = _ipHelper.GetRegion(a.IP);
        });

        return new PageDto<RecentAccessHistory>()
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }
}