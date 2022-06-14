using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using Panda.Tools.Exception;

namespace Panda;

public static class ObjectExtensions
{
    public static void IsNullThrow(this object? obj, string message)
    {
        if (obj == null) throw new UserException(message);
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static byte[] ToBytes(object obj)
    {
        using var memoryStream = new MemoryStream();
        var ser = new DataContractSerializer(typeof(object));
        ser.WriteObject(memoryStream, obj);
        var data = memoryStream.ToArray();
        return data;
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    private static T Deserialize<T>(byte[] data)
    {
        using var memoryStream = new MemoryStream(data);
        var reader =
            XmlDictionaryReader.CreateTextReader(memoryStream, new XmlDictionaryReaderQuotas());
        var ser = new DataContractSerializer(typeof(T));
        var result = (T) ser.ReadObject(reader, true)!;
        return result;
    }
}