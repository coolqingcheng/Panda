using Microsoft.Extensions.Caching.Distributed;
using Panda.Services.Cache;
using PandaTools.Helper;

namespace Panda.Services
{
    public class FriendLinkService
    {
        private readonly DbContext _context;

        private readonly IDistributedCache _cache;


        public FriendLinkService(DbContext context, IDistributedCache client)
        {
            _context = context;
            _cache = client;

        }

        public async Task<PageDto<FriendLinkItem>> GetLinkList(GetFriendLinkModel model)
        {

            var cache = await _cache.GetModelAsync<PageDto<FriendLinkItem>>(CacheKeys.FriendLinkKey);
            if (cache != null)
            {
                return cache;
            }
            var list = await _context.Set<FriendLink>().AsNoTracking().Where(a => a.IsPublish).Skip(model.Skip).Take(model.PageSize)
                .OrderByDescending(a => a.Order)
                .Select(a => new FriendLinkItem()
                {
                    Name = a.Name,
                    Url = a.Url
                }).ToListAsync();
            var dto = new PageDto<FriendLinkItem>(await _context.Set<FriendLink>().AsNoTracking().CountAsync(), list);
            await _cache.SetModelAsync(CacheKeys.FriendLinkKey, dto, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            });
            return dto;
        }
    }
}
