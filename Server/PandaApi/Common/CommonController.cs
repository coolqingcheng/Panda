using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Panda.Models.Data.Entitys;

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
    ///     上传
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Upload([FromServices] IWebHostEnvironment host)
    {
        var files = Request.Form.Files;
        var size = files.Sum(f => f.Length);
        var list = new List<string>();
        foreach (var formFile in files)
            if (formFile.Length > 0)
            {
                var path = Path.Combine(host.WebRootPath, "files");

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(formFile.FileName)}";
                path = Path.Combine(path, fileName);
                var filePath = path;

                using var stream = System.IO.File.Create(filePath);
                await formFile.CopyToAsync(stream);
                var c = Path.VolumeSeparatorChar;
                var file =
                    $"/{Path.Combine("files", fileName).Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)}";
                list.Add(file);
                await _dbcontext.Set<SysResource>().AddAsync(new SysResource
                {
                    Path = file,
                    Size = new FileInfo(filePath).Length,
                    Suffix = Path.GetExtension(formFile.FileName),
                    ClientIp = HttpContext.GetClientIP(),
                    StorageType = StorageType.Local
                });
            }

        await _dbcontext.SaveChangesAsync();

        return Ok(new { list, size });
    }
}