using Mapster;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Repository.FriendlyLink;
using Panda.Tools.Extensions;

namespace Panda.Services.FriendlyLink;

public interface IFriendlyLinkService
{
    Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request);
}

public class FriendlyLinkService : IFriendlyLinkService
{
    private readonly FriendLinkRepository _friendLinkRepository;

    public FriendlyLinkService(FriendLinkRepository friendLinkRepository)
    {
        _friendLinkRepository = friendLinkRepository;
    }

    public async Task<PageDto<FriendlyLinkResponse>> GetList(FriendlyLinkRequest request)
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
}