using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using QingCheng.Site.Api.Blogs.Services.Models;
using QingCheng.Site.Services.Cache;
using QingCheng.Site.Data.Blogs;
using QingCheng.Site.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace QingCheng.Site.Api.Blogs.Services
{
    public class FriendLinkService
    {
        private readonly QingChengContext _context;

        readonly IDistributedCache _cache;

        public FriendLinkService(QingChengContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<PageDto<FriendLinkModel>> GetList(FriendLinkRequestModel request)
        {
            var list = await _context.FriendLinks.Skip(request.Skip).Take(request.PageSize).Select(a => new FriendLinkModel()
            {
                Name = a.Name,
                Url = a.Url,
                Order = a.Order,
                Id = a.Id,
                IsPublish = a.IsPublish
            }).ToListAsync();
            return new PageDto<FriendLinkModel>(await _context.FriendLinks.CountAsync(), list);
        }

        public async Task Edit(FriendLinkModel model)
        {
            if (model.Id > 0)
            {
                var item = await _context.FriendLinks.FirstOrDefaultAsync(a => a.Id == model.Id);
                if (item != null)
                {
                    model.Adapt(item);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var item = model.Adapt<FriendLink>();
                await _context.FriendLinks.AddAsync(item);
                await _context.SaveChangesAsync();

            }
            await _cache.RemoveAsync(CacheKeys.FriendLinkKey);
        }
    }
}
