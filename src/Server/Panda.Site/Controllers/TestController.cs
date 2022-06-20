using Microsoft.AspNetCore.Mvc;

namespace Panda.Site.Controllers;

public class TestController : Controller
{
    // GET
    [HttpGet("/test")]
    public string Index()
    {
        return "time:"+DateTime.Now;
    }
}