using System.ComponentModel.DataAnnotations;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Panda.Models;
using Panda.Models.Data.Entitys;
using CaptchaHelper = PandaTools.Helper.CaptchaHelper;

namespace PandaApi.Common;

/// <summary>
///     公共操作
/// </summary>
public class CommonController : BaseAdminController
{
    private readonly DbContext _dbcontext;

    public CommonController(DbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="host"></param>
    /// <param name="isTag"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Upload([FromServices] IWebHostEnvironment host, [FromQuery] bool isTag = true)
    {
        var files = Request.Form.Files;
        var size = files.Sum(f => f.Length);
        var list = new List<string>();
        foreach (var formFile in files)
            if (formFile.Length > 0)
            {
                var path = Path.Combine(host.WebRootPath, "files", DateTime.Now.ToString("yyyyMMdd"));

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(formFile.FileName)}";
                path = Path.Combine(path, fileName);
                var filePath = path;
                using var ms = new MemoryStream();
                await using var stream = System.IO.File.Create(filePath);
                if (isTag)
                {
                    ImageHelper.WriteWaterTag(ms.ToArray(), "iwscl.com", 10);
                }
                else
                {
                    await formFile.CopyToAsync(ms);
                }

               
                var file =
                    $"/{Path.Combine("files", fileName).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)}";
                list.Add(file);
                await _dbcontext.Set<SysResource>().AddAsync(new SysResource
                {
                    Path = file,
                    Size = new FileInfo(filePath).Length,
                    Suffix = Path.GetExtension(formFile.FileName),
                    ClientIp = HttpContext.GetClientIp(),
                    StorageType = StorageType.Local
                });
            }

        await _dbcontext.SaveChangesAsync();

        return Ok(new { list, size });
    }

    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="captcha"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpGet, AllowAnonymous]
    public IActionResult GetCaptcha([FromServices] ICaptcha captcha, [Required] ValidateCodeType type)
    {
        var (id, guid) = CaptchaHelper.GenerateId((int)type);
        var info = captcha.Generate(id, 60 * 3);
        // 有多处验证码且过期时间不一样，可传第二个参数覆盖默认配置。
        //var info = _captcha.Generate(id,120);
        var stream = new MemoryStream(info.Bytes);
        HttpContext.Response.Cookies.Append("captcha-id", guid);
        return File(stream, "image/gif");
    }
}