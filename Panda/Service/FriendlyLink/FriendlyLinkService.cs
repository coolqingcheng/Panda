using Mapster;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Repository.FriendlyLink;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;

namespace Panda.Services.FriendlyLink;

public class FriendlyLinkService : IFriendlyLinkService
{
    private readonly FriendLinkRepository _friendLinkRepository;

    public FriendlyLinkService(FriendLinkRepository friendLinkRepository)
    {
        _friendLinkRepository = friendLinkRepository;
    }



    public async Task<PageDto<FriendlyLinkResponse>> AdminGetList(FriendlyLinkRequest request)
    {
        var query = _friendLinkRepository.Queryable();
        var list = await query.Page(request).OrderByDescending(a => a.Weight).ProjectToType<FriendlyLinkResponse>()
            .ToListAsync();

        return new PageDto<FriendlyLinkResponse>()
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }

    public async Task Audit(int Id, AuditStatusEnum auditStatus)
    {
        var item = await _friendLinkRepository.Where(a => a.Id == Id).FirstOrDefaultAsync();
        if (item != null)
        {
            item.AuditStatus = auditStatus;
            await _friendLinkRepository.SaveAsync();
        }
    }

    public async Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request)
    {
        var query = _friendLinkRepository.Where(a => a.AuditStatus == AuditStatusEnum.Pass);
        var list = await query.Page(request).OrderByDescending(a => a.Weight).ProjectToType<FriendlyLinkResponse>()
            .ToListAsync();

        return new PageDto<FriendlyLinkResponse>()
        {
            Total = await query.CountAsync(),
            Data = list
        };
    }


    public async Task AddFriendLink(AddFriendLinkRequest request, AuditStatusEnum auditStatus = AuditStatusEnum.unaudit)
    {
        await _friendLinkRepository.AddAsync(new FriendlyLinks()
        {
            SiteName = request.Name,
            SiteUrl = request.Url
        });
    }
}