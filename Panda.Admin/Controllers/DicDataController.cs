using Microsoft.AspNetCore.Mvc;
using Panda.Admin.Models.Request;
using Panda.Admin.Services.DicData;
using Panda.Services.DicData;

namespace Panda.Admin.Controllers;

public class DicDataController : AdminController
{
    private readonly IDicDataService _dicDataService;

    public DicDataController(IDicDataService dicDataService)
    {
        _dicDataService = dicDataService;
    }

    [HttpPost]
    public async Task Update(DicUpdateRequest request)
    {
        var dto = new DicDataValidator().Validate(request);
        await _dicDataService.AddOrUpdate(dto);
    }

    [HttpGet]
    public async Task<Dictionary<string, string?>> Get(string groupName)
    {
        var list =  await _dicDataService.GetItemByGroupName(groupName);

        return list.ToDictionary(info => info.Key, info => info.Value);
    }
}