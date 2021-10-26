using Panda.Entity.Requests;
using Panda.Entity.Responses;

namespace Panda.Services.Pages;

public interface IPageService
{
    Task AddOrUpdate(PagesRequest request);

    Task<PageDto<PagesItem>> GetPagesList(GetPagesRequest request);
}