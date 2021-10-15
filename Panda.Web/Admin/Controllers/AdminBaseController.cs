using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Panda.Web.Admin.Controllers;

/// <summary>
/// 后台api
/// </summary>
[Route("/admin/[controller]/[action]")]
[Authorize]
public class AdminBaseController:Controller
{
    
}