using Panda.Tools.Web;

namespace Panda.Admin.Services.DicData;

public class EFDicDataProvider : IDicDataProvider
{
    private readonly IDicDataService _dicDataService;

    public EFDicDataProvider(IDicDataService dicDataService)
    {
        _dicDataService = dicDataService;
    }

    private string DefaultGroupName { get; set; }

    public void SetDefaultGroupName(string groupName)
    {
        DefaultGroupName = groupName;
    }

    public async Task<string?> GetDefaultGroupName(string key)
    {
        var item = await _dicDataService.GetItemByCache(DefaultGroupName, key);
        return item?.Value;
    }

    public async Task<string?> Get(string group, string key)
    {
        var item = await _dicDataService.GetItemByCache(group, key);
        return item?.Value;
    }
}