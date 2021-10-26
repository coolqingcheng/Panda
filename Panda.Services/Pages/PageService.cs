using Mapster;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.Requests;
using Panda.Entity.Responses;
using Panda.Repository.Page;
using Panda.Tools.Exception;
using Panda.Tools.Extensions;

namespace Panda.Services.Pages;

public class PageService : IPageService
{
    private readonly PageRepository _pageRepository;

    public PageService(PageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async Task AddOrUpdate(PagesRequest request)
    {
        if (request.Id>0)
        {
            var item =  await _pageRepository.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
            if (item == null)
                throw new UserException("保存的ID不存在");
            request.Adapt(item);
            await _pageRepository.SaveAsync();
        }
        else
        {
            var any =  await _pageRepository.Where(a => a.Url == request.Url).AnyAsync();
            if (any)
            {
                throw new UserException("url已经存在，无法添加");
            }
            var item =  request.Adapt<Entity.DataModels.Pages>();
            await _pageRepository.AddAsync(item);
        }
    }

    public async Task<PageDto<PagesItem>> GetPagesList(GetPagesRequest request)
    {
        var query = _pageRepository.Queryable();
        var list = await query.Page(request).ProjectToType<PagesItem>().ToListAsync();
        return new PageDto<PagesItem>()
        {
            Total = query.Count(),
            Data = list
        };
    }
}