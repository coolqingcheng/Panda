using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using Panda.Tools.Exception;
using TencentCloud.Iotcloud.V20210408.Models;

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
        var result = (T)ser.ReadObject(reader, true)!;
        return result;
    }

    public static void SetValue(this object obj, PropertyInfo propertyInfo, object value)
    {
        if (propertyInfo.PropertyType == typeof(string))
        {
            propertyInfo.SetValue(obj, value.ToString());
        }

        if (propertyInfo.PropertyType == typeof(int))
        {
            var temp = Convert.ToInt32(value);
            propertyInfo.SetValue(obj, temp);
        }

        if (propertyInfo.PropertyType == typeof(long))
        {
            var temp = Convert.ToInt64(value);
            propertyInfo.SetValue(obj, temp);
        }

        if (propertyInfo.PropertyType == typeof(DateTime))
        {
            var temp = Convert.ToDateTime(value);
            propertyInfo.SetValue(obj, temp);
        }

        if (propertyInfo.PropertyType == typeof(bool))
        {
            var temp = Convert.ToBoolean(value);
            propertyInfo.SetValue(obj, temp);
        }
    }
}