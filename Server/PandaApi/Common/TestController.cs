using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace PandaApi.Common;

public class TestController : Controller
{
    [HttpGet("/TestWaterTag")]
    public async Task<IActionResult> TestWaterTag([FromServices] IWebHostEnvironment environment,
        string name = "iwscl.com")
    {
        var filePath = Path.Combine(environment.WebRootPath, "img", "test.jpg");
        await using var fs = System.IO.File.Open(filePath, FileMode.Open);
        using var ms = new MemoryStream();
        await fs.CopyToAsync(ms);
        var img = ImageHelper.WriteWaterTag(ms.ToArray(), name, 80);
        var ms2 = new MemoryStream(img);
        return File(ms2, "image/jpg");
    }
}