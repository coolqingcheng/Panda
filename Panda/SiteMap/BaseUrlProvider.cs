using Panda.Admin.Services.DicData;
using SimpleMvcSitemap.Routing;

namespace Panda.SiteMap;

public class BaseUrlProvider : IBaseUrlProvider
{
    public BaseUrlProvider(IDicDataService _dicDataService)
    {
        BaseUrl = new Uri(_dicDataService.GetItemByCache("site:host").GetAwaiter().GetResult()!);
    }

    public Uri BaseUrl { get; }
}