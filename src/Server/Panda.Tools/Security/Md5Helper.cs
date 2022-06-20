using System.Security.Cryptography;
using System.Text;

namespace Panda.Tools.Security;

public class Md5Helper
{
    private static MD5? _sMd5;

    private static MD5 Md5Obj => _sMd5 ??= MD5.Create();


    public static string ComputeHash(byte[]? buffer)
    {
        if (buffer == null || buffer.Length < 1)
            return "";

        var hash = Md5Obj.ComputeHash(buffer);
        var sb = new StringBuilder();
        foreach (var b in hash)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }

    public static string ComputeHash(string str)
    {
        var buffer = Encoding.UTF8.GetBytes(str);
        return ComputeHash(buffer);
    }
}