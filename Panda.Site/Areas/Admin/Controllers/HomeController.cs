using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Panda.Site.Areas.Admin.Controllers;

public class HomeController : Controller
{
    /// <summary>
    ///     后台Spa页面
    /// </summary>
    /// <param name="env"></param>
    /// <returns></returns>
    [HttpGet("/admin")]
    [AllowAnonymous]
    public ActionResult AdminHtml([FromServices] IWebHostEnvironment env)
    {
        var path = Path.Combine(env.WebRootPath, "admin", "index.html");
        if (System.IO.File.Exists(path) == false) return Content("后台文件找不到，请确定已经发布");

        return PhysicalFile(path, "text/html; charset=utf-8");
    }
}