using System.Text.RegularExpressions;

namespace Panda.Tools.Web;

public class Base64Utils
{
    /// <summary>
    ///     获取base64字符串的mime信息
    /// </summary>
    /// <param name="base64"></param>
    /// <returns></returns>
    public static string GetBase64Mime(string base64)
    {
        var result = Regex.Match(base64, "(?<=data:).+(?=;)");
        return result.Value;
    }

    public static string GetBase64byImage(string imgBase64)
    {
        var result = Regex.Match(imgBase64, @"(?<=\,).+");
        return result.Value;
    }
}