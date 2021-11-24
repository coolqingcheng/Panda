using EasyCaching.Core;
using EasyCaching;
using Microsoft.AspNetCore.Mvc;
using Panda.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Panda.Admin.Controllers
{
    public class CacheController : AdminBaseController
    {
        [AllowAnonymous]
        [HttpGet("/cache")]
        public IActionResult Index()
        {
            var a = CacheKeys.NoticeCacheKey;
            return View();
        }
    }
}
