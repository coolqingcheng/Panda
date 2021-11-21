namespace Panda.Tools.Web;

public class MimeUtils
{
    /// <summary>
    /// 获取Mime的后缀名
    /// </summary>
    /// <param name="mimeName"></param>
    /// <returns></returns>
    public static string GetMimeExtName(string mimeName)
    {
        return mimeName switch
        {
            "image/png" => "png",
            "image/jpg" => "jpeg",
            "image/jpeg" => "jpeg",
            "image/gif" => "gif",
            "image/x-ms-bmp" => "bmp",
            _ => throw new ArgumentOutOfRangeException(nameof(mimeName), mimeName, $"暂时不支持当前类型：{mimeName}")
        };
    }
}