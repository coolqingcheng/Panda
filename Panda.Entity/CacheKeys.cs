using Panda.Models;
using Panda.Tools.Exception;

namespace Panda.Entity;

/// <summary>
/// 缓存Key
/// 需要拼接key的，以下划线结尾
/// </summary>
public class CacheKeys
{
    /// <summary>
    /// 验证重复Key
    /// </summary>
    /// <returns></returns>
    public static void ValidteKeyRepetition()
    {
        var item = typeof(CacheKeys).GetFields().Select(a => a.GetValue(a))
            .GroupBy(a => a)
            .Select(a => new { a, count = a.Count() })
            .Where(a => a.count > 1).FirstOrDefault();
        if (item != null)
        {
            throw new ValidateFailException($"缓存key有重复，重复项为：{ item.a }");
        }
    }
    /// <summary>
    /// 所有分类
    /// </summary>
    public const string Categories = "Categories";

    /// <summary>
    /// 字典
    /// </summary>
    public const string DicDataGroupNameKey = "DicData_";


    /// <summary>
    /// 通知缓存
    /// </summary>
    public const string NoticeCacheKey = "NoticeCacheKey";

}