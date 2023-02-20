﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QingCheng.Site.Api.Spa;

/// <summary>
/// 处理Spa文件
/// </summary>
public class SpaController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SpaController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet("/ef-db")]
    public async Task<ActionResult> MigrationEF([FromServices] DbContext context)
    {
        await context.Database.MigrateAsync();
        return Content("迁移成功:" + DateTime.Now);
    }

    /// <summary>
    /// 后台Spa页面
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