using System.Text.Json;

namespace Panda;

public static class StringExtension
{
    public static string ToJson(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return json;
    }


    public static T? JsonToObj<T>(this string? json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return default;
        }
        var res = JsonSerializer.Deserialize<T>(json);
        return res;
    }

    public static T? JsonToObj<T>(this byte[] json)
    {
        var res = JsonSerializer.Deserialize<T>(json);
        return res;
    }
}