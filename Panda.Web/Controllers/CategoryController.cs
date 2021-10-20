using Microsoft.AspNetCore.Mvc;

namespace Panda.Web.Controllers;

public class CategoryController : Controller
{
    // GET
    [HttpGet("/category/{id:int}")]
    public IActionResult Index(int id)
    {
        return View();
    }
}