using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Panda.Admin.Controllers;

/// <summary>
///     后台api
/// </summary>
[Route("/admin/[controller]/[action]")]
[Authorize]
[ApiController]
public class AdminController : Controller
{
}