using EasyCaching.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Repository.FriendlyLink;
using Panda.Tools.Extensions;

namespace Panda.Services.FriendlyLink;

public class FriendlyLinkService : IFriendlyLinkService
{
    private readonly IEasyCachingProvider _easyCachingProvider;
    private readonly FriendLinkRepository _friendLinkRepository;

    public FriendlyLinkService(FriendLinkRepository friendLinkRepository, IEasyCachingProvider easyCachingProvider)
    {
        _friendLinkRepository = friendLinkRepository;
        _easyCachingProvider = easyCachingProvider;
    }


    public async Task<PageDto<FriendlyLinkResponse>> AdminGetList(FriendlyLinkRequest request)
    {
        var query = _friendLinkRepository.Queryable();
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
        var res = await _easyCachingProvider.GetAsync(
            CacheKeys.FriendLinkList + request.Index,
            async () => await GetList(request), TimeSpan.FromHours(2));
        return res.Value;
    }

    public async Task Delete(int id)
    {
        await _friendLinkRepository.DeleteWhereAsync(a => a.Id == id);
        await _easyCachingProvider.RemoveByPrefixAsync(CacheKeys.FriendLinkList);
    }

    public async Task<FriendlyLinkResponse?> Get(int id)
    {
        var item = await _friendLinkRepository.Where(a => a.Id == id).ProjectToType<FriendlyLinkResponse>()
            .FirstOrDefaultAsync();
        return item;
    }

    public async Task Audit(int id, AuditStatusEnum auditStatus)
    {
        var item = await _friendLinkRepository.Where(a => a.Id == id).FirstOrDefaultAsync();
        if (item != null)
        {
            item.AuditStatus = auditStatus;
            await _friendLinkRepository.SaveAsync();
            await _easyCachingProvider.RemoveByPrefixAsync(CacheKeys.FriendLinkList);
        }
    }

    public async Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request)
    {
        var query = _friendLinkRepository.Where(a => a.AuditStatus == AuditStatusEnum.Pass);
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
        var item = await _friendLinkRepository.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
        if (item == null)
        {
            item = new FriendlyLinks
            {
                SiteName = request.siteName,
                SiteUrl = request.siteUrl,
                AuditStatus = request.AuditStatus ?? AuditStatusEnum.unaudit
            };
            await _friendLinkRepository.AddAsync(item);
        }
        else
        {
            item.SiteUrl = request.siteUrl;
            item.SiteName = request.siteName;
            if (request.AuditStatus != null) item.AuditStatus = (AuditStatusEnum) request.AuditStatus;

            await _friendLinkRepository.SaveAsync();
        }

        await _easyCachingProvider.RemoveByPrefixAsync(CacheKeys.FriendLinkList);
    }
}