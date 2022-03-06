using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.Service.BackUp;

namespace Panda.Admin.Controllers;

public class BackUpController : AdminController
{
    private readonly IBackUpService _backUpService;

    public BackUpController(IBackUpService backUpService)
    {
        _backUpService = backUpService;
    }
    [HttpGet]
    public void BackUpMysql()
    {
        _backUpService.BackUpMysql();
    }
}