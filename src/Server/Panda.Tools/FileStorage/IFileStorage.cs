namespace Panda.Tools.FileStorage;

public interface IFileStorage
{
    Task<UploadFileResult> SaveAsync(byte[] bytes, string name);
}

/// <summary>
///     文件上传结果
/// </summary>
public class UploadFileResult
{
    public bool Success { get; set; }

    public string Message { get; set; }

    /// <summary>
    ///     地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     大小
    /// </summary>
    public long Size { get; set; }
}