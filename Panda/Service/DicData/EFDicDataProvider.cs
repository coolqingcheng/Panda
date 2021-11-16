using Panda.Entity.Requests;
using Panda.Tools.Web;

namespace Panda.Services.DicData;

public class EFDicDataProvider : IDicDataProvider
{
    public EFDicDataProvider(IDicDataService dicDataService)
    {
        _dicDataService = dicDataService;
    }

    private string DefaultGroupName { get; set; }

    private readonly IDicDataService _dicDataService;

    public void SetDefaultGroupName(string groupName)
    {
        DefaultGroupName = groupName;
    }

    public async Task<string?> GetDefaultGroupName(string key)
    {
        var item = await _dicDataService.GetItemByCache(DefaultGroupName, key);
        return item?.Value;
    }

    public async Task<string?> Get(string @group, string key)
    {
        var item = await _dicDataService.GetItemByCache(@group, key);
        return item?.Value;
    }
}