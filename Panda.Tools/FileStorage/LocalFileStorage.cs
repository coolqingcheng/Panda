using Microsoft.AspNetCore.Hosting;

namespace Panda.Tools.FileStorage;

public class LocalFileStorage : IFileStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalFileStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<UploadFileResult> SaveAsync(byte[] bytes, string name)
    {
        var yyyyMMdd = DateTime.Now.ToString("yyyyMMdd");
        var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Content", "Upload", yyyyMMdd);
        if (Directory.Exists(localPath) == false)
        {
            Directory.CreateDirectory(localPath);
        }
        await File.WriteAllBytesAsync(Path.Combine(localPath, name),bytes);
        return new UploadFileResult()
        {
            Size = bytes.Length,
            Url = $"/img/{yyyyMMdd}/{name}",
            Success = true
        };
    }
}