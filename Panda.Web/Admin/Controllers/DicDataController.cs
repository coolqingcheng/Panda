using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity.Requests;
using Panda.Services.DicData;

namespace Panda.Web.Admin.Controllers;

public class DicDataController : AdminBaseController
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
}