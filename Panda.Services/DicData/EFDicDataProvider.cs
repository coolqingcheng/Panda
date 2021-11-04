using Panda.Tools.Web;

namespace Panda.Services.DicData;

public class EFDicDataProvider : IDicDataProvider
{
    public EFDicDataProvider(IDicDataService dicDataService)
    {
        _dicDataService = dicDataService;
    }

    private string DefaultGroupName { get; set; }

    private IDicDataService _dicDataService;

    public void SetDefaultGroupName(string groupName)
    {
        DefaultGroupName = groupName;
    }

    public async Task<string> GetDefaultGroupName(string key)
    {
        var item = await _dicDataService.GetItem(DefaultGroupName, key);
        return item.Value;
    }

    public async Task<string> Get(string @group, string key)
    {
        var item = await _dicDataService.GetItem(@group, key);
        return item.Value;
    }
}