using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Panda.Tools.Exception;

namespace Panda;

public static class ObjectExtensions
{
    public static void IsNullThrow(this object? obj, string message)
    {
        if (obj == null) throw new UserException(message);
    }

    /// <summary> 
    /// 将一个object对象序列化，返回一个byte[]         
    /// </summary> 
    /// <param name="obj">能序列化的对象</param>         
    /// <returns></returns> 
    public static byte[] ToBytes(this object obj)
    {
        using var ms = new MemoryStream();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        return ms.GetBuffer();
    }
}