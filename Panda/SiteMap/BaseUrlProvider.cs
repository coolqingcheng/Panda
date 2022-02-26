using Panda.Services.DicData;
using SimpleMvcSitemap.Routing;

namespace Panda.SiteMap;

public class BaseUrlProvider : IBaseUrlProvider
{
    public Uri BaseUrl { get; }

    public BaseUrlProvider(IDicDataService _dicDataService)
    {
        BaseUrl = new Uri(_dicDataService.GetItemByCache("site:host").GetAwaiter().GetResult()!);
    }
}