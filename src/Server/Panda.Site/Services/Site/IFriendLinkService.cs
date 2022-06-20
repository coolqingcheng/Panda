using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Responses;
using Panda.Site.Services.Site.Models;
using Panda.Tools.Extensions;

namespace Panda.Site.Services.Site;

public interface IFriendLinkService
{
    Task<PageDto<FriendLinkModel>> GetListByPassAsync(int pageIndex, int pageSize);
}

public class FriendLinkService : IFriendLinkService
{
    private readonly DbContext _dbContext;

    public FriendLinkService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PageDto<FriendLinkModel>> GetListByPassAsync(int pageIndex, int pageSize)
    {
        var query = _dbContext.Set<FriendlyLinks>().Where(a => a.AuditStatus == AuditStatusEnum.Pass);
        var list = await query.OrderByDescending(a => a.Weight).ThenByDescending(a => a.AddTime)
            .Page(pageIndex, pageSize).Select(a => new FriendLinkModel()
            {
                ShowName = a.SiteName,
                Url = a.SiteUrl
            }).ToListAsync();
        return new PageDto<FriendLinkModel>()
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }
}