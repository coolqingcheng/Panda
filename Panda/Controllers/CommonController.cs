using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Panda.Tools.Exception;
using System;
using System.Runtime.InteropServices;
using Panda.Models;
using Panda.Services.Account;
using Panda.Tools.FileStorage;
using Panda.Tools.Models;
using Panda.Tools.Security;
using Panda.Tools.Web;

namespace Panda.Controllers;

[ApiController]
public class CommonController : Controller
{
    private readonly IFileStorage _fileStorage;
    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly IAccountService _accountService;

    public CommonController(IFileStorage fileStorage, IWebHostEnvironment webHostEnvironment,
        IAccountService accountService)
    {
        _fileStorage = fileStorage;
        _webHostEnvironment = webHostEnvironment;
        _accountService = accountService;
    }

    [IgnoreAntiforgeryToken]
    [HttpPost("/upload")]
    public async Task<UploadResult> Upload(IFormCollection form)
    {
        var files = form.Files;
        if (files.Count == 0)
        {
            throw new UserException("服务器没有收到文件");
        }

        if (files[0].Length >= (1024 * 1024 * 8)) //图片不能大于4M
        {
            throw new UserException("图片不能大于4M");
        }

        var list = new List<string>() {"jpg", "png", "jpeg", "gif"};
        var regex = Regex.Match(files[0].FileName, @"[^\.]\w*$");
        if (regex.Success == false || list.Contains(regex.Value) == false)
        {
            throw new UserException("文件格式不支持！");
        }

        await using var stream = files[0].OpenReadStream();
        var buffer = new byte[stream.Length];
        var readAsync = await stream.ReadAsync(buffer);
        // todo 图片水印功能
        var file = await _fileStorage.SaveAsync(buffer, $"{Md5Helper.ComputeHash(buffer)[..10]}.{regex.Value}");
        if (file.Success == false)
        {
            return new UploadResult {Code = 1, Message = file.Message};
        }

        return new UploadResult
        {
            Code = 0,
            Url = file.Url
        };
    }

    [IgnoreAntiforgeryToken]
    [HttpPost("/uploadbase64")]
    public async Task<UploadResult> UploadBase64(UploadBase64Model request)
    {
        var bytes = Convert.FromBase64String(Base64Utils.GetBase64byImage(request.Base64));
        request.Base64 = ImageUtils.GetPicThumbnail(bytes, request.W, request.H);
        var mime = Base64Utils.GetBase64Mime(request.Base64);
        var extName = MimeUtils.GetMimeExtName(mime);
        ;
        var file = await _fileStorage.SaveAsync(bytes, $"{Md5Helper.ComputeHash(bytes)[..10]}.{extName}");
        if (file.Success == false)
        {
            return new UploadResult {Code = 1, Message = file.Message};
        }

        return new UploadResult()
        {
            Code = 0,
            Url = file.Url
        };
    }

    [HttpGet("/img/{day}/{file}")]
    public ActionResult Img(string day, string file)
    {
        // todo 防盗链处理
        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Content", "Upload", day, file);
        var ext = new FileInfo(path).Extension;
        return PhysicalFile(path, $"image/{ext[1..]}");
    }

    [HttpGet("/initadmin")]
    public async Task<IActionResult> InitAdminAsync()
    {
        await _accountService.InitAdminPassword();
        return Content("ok:" + DateTime.Now);
    }
}