using Microsoft.AspNetCore.Mvc;
using Panda.Admin.Models;
using Panda.Service.DashBoard;

namespace Panda.Admin.Controllers;

public class DashBoardController : AdminController
{
    private readonly IDashBoardService _dashBoardService;

    public DashBoardController(IDashBoardService dashBoardService)
    {
        _dashBoardService = dashBoardService;
    }

    /// <summary>
    /// 后台主页统计数据
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<DashBoardStatisticModel> Statistic()
    {
        return _dashBoardService.GetStatistic();
    }
}