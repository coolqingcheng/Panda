using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace Panda.Tools.Extensions;

public static class StringExtension
{
    public static string ToJson(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return json;
    }
    
    

    public static T? JsonToObj<T>(this string json)
    {
        var res = JsonSerializer.Deserialize<T>(json);
        return res;
    }
    
    public static T? JsonToObj<T>(this byte[] json)
    {
        var res = JsonSerializer.Deserialize<T>(json);
        return res;
    }
}