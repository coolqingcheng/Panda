using Panda.Entity.Responses;
using Panda.Repository.Notify;
using Mapster;
using Microsoft.EntityFrameworkCore;
using EasyCaching.Core;
using Panda.Entity;
using Panda.Entity.Requests;
using Panda.Tools.Extensions;

namespace Panda.Service.Notify
{
    public interface INoticeService
    {
        Task<IEnumerable<NoticeResponse>> GetNoticeByCache(int top);

        Task<IEnumerable<NoticeResponse>> GetNoticeList(NoticeRequest request);

        Task SetTop(int Id);
    }

    public class NoticeService : INoticeService
    {
        private readonly NoticeRepository _noticeRepository;

        private readonly IEasyCachingProvider _easyCachingProvider;

        public NoticeService(NoticeRepository noticeRepository, IEasyCachingProvider easyCachingProvider)
        {
            _noticeRepository = noticeRepository;
            _easyCachingProvider = easyCachingProvider;
        }

        public async Task<IEnumerable<NoticeResponse>> GetNoticeByCache(int top)
        {
            var res = await _easyCachingProvider.GetAsync<IEnumerable<NoticeResponse>>(CacheKeys.NoticeCacheKey, async () =>
           {
               var list = await _noticeRepository.Queryable().OrderByDescending(a => a.IsTop).ThenByDescending(a => a.AddTime).Take(top).ProjectToType<NoticeResponse>().ToListAsync();
               return list;
           }, TimeSpan.FromMinutes(10));
            return res.Value;
        }

        public async Task<IEnumerable<NoticeResponse>> GetNoticeList(NoticeRequest request)
        {
            var list = await _noticeRepository.Queryable().Page(request).OrderByDescending(a => a.IsTop).ThenByDescending(a => a.AddTime).ProjectToType<NoticeResponse>().ToListAsync();
            return list;
        }

        public async Task SetTop(int Id)
        {
            var item = await _noticeRepository.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (item != null)
            {
                item.IsTop = !item.IsTop;
                await _noticeRepository.SaveAsync();
            }
        }
    }
}
