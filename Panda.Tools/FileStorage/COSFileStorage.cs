namespace Panda.Tools.FileStorage;

/// <summary>
/// 腾讯云COS存储
/// </summary>
public class CosFileStorage : IFileStorage
{
    public Task<UploadFileResult> SaveAsync(byte[] bytes, string name)
    {
        throw new NotImplementedException();
    }
}