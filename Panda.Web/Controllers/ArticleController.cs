using Microsoft.AspNetCore.Mvc;

namespace Panda.Web.Controllers;

public class ArticleController : Controller
{
    // GET
    [HttpGet("/article/{Id:int}.html")]
    public IActionResult Index()
    {
        return View();
    }
}