using Mapster;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Tools.Extensions;

namespace Panda.Site.Areas.Admin.Services.FriendlyLink;

public interface IFriendlyLinkService
{
    Task<PageDto<FriendlyLinkResponse>> AdminGetList(FriendlyLinkRequest request);


    Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request);

    Task<PageDto<FriendlyLinkResponse>> GetListByCache(FriendlyLinkRequest request);


    Task Delete(int id);

    Task<FriendlyLinkResponse?> Get(int id);


    Task Audit(int id, AuditStatusEnum auditStatus);


    Task AddOrUpdateFriendLink(AddFriendLinkRequest request);
}

public class FriendlyLinkService : IFriendlyLinkService
{
    private readonly DbContext _dbContext;

    public FriendlyLinkService(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<PageDto<FriendlyLinkResponse>> AdminGetList(FriendlyLinkRequest request)
    {
        var query = _dbContext.Set<FriendlyLinks>().AsQueryable();
        var list = await query.Page(request).OrderByDescending(a => a.Weight).ProjectToType<FriendlyLinkResponse>()
            .ToListAsync();

        return new PageDto<FriendlyLinkResponse>
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public async Task<PageDto<FriendlyLinkResponse>> GetListByCache(FriendlyLinkRequest request)
    {
        return await GetList(request);
    }

    public async Task Delete(int id)
    {
        var item = await _dbContext.Set<FriendlyLinks>().FirstOrDefaultAsync(a => a.Id == id);
        if (item != null) _dbContext.Set<FriendlyLinks>().Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FriendlyLinkResponse?> Get(int id)
    {
        var item = await _dbContext.Set<FriendlyLinks>().Where(a => a.Id == id).ProjectToType<FriendlyLinkResponse>()
            .FirstOrDefaultAsync();
        return item;
    }

    public async Task Audit(int id, AuditStatusEnum auditStatus)
    {
        var item = await _dbContext.Set<FriendlyLinks>().Where(a => a.Id == id).FirstOrDefaultAsync();
        if (item != null)
        {
            item.AuditStatus = auditStatus;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request)
    {
        var query = _dbContext.Set<FriendlyLinks>().Where(a => a.AuditStatus == AuditStatusEnum.Pass);
        var list = await query.Page(request).OrderByDescending(a => a.Weight).ProjectToType<FriendlyLinkResponse>()
            .ToListAsync();

        return new PageDto<FriendlyLinkResponse>
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }


    public async Task AddOrUpdateFriendLink(AddFriendLinkRequest request)
    {
        var item = await _dbContext.Set<FriendlyLinks>().Where(a => a.Id == request.Id).FirstOrDefaultAsync();
        if (item == null)
        {
            item = new FriendlyLinks
            {
                SiteName = request.siteName,
                SiteUrl = request.siteUrl,
                AuditStatus = request.AuditStatus ?? AuditStatusEnum.Unaudit,
                Weight = request.Weight
            };
            await _dbContext.Set<FriendlyLinks>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            item.SiteUrl = request.siteUrl;
            item.SiteName = request.siteName;
            item.Weight = request.Weight;
            if (request.AuditStatus != null) item.AuditStatus = (AuditStatusEnum) request.AuditStatus;

            await _dbContext.SaveChangesAsync();
        }
    }
}