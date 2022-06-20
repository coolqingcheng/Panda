using System.ComponentModel;
using System.Reflection;
using Panda.Tools.Exception;

namespace Panda.Entity;

/// <summary>
///     缓存Key
///     需要拼接key的，以下划线结尾
/// </summary>
public class CacheKeys
{
    /// <summary>
    ///     所有分类
    /// </summary>
    [Description("分类")] public const string Categories = "Categories";

    /// <summary>
    ///     友情链接列表
    /// </summary>
    [Description("友情链接列表")] public const string FriendLinkList = "FriendLinkList";

    /// <summary>
    ///     字典
    /// </summary>
    [Description("字典")] public const string DicDataGroupNameKey = "DicData_";


    /// <summary>
    ///     通知缓存
    /// </summary>
    [Description("通知")] public const string NoticeCacheKey = "NoticeCacheKey";


    /// <summary>
    ///     验证码前缀
    /// </summary>
    [Description("验证码")] public const string ValidateCode = "ValidateCode_";

    /// <summary>
    ///     验证重复Key
    /// </summary>
    /// <returns></returns>
    public static void ValidateKeyRepetition()
    {
        var item = typeof(CacheKeys).GetFields()
            .Select(a => a.GetValue(a))
            .GroupBy(a => a)
            .Select(a => new {a, count = a.Count()}).FirstOrDefault(a => a.count > 1);
        if (item != null) throw new ValidateFailException($"缓存key有重复，重复项为：{item.a}");
    }

    public static Dictionary<string, string?> GetAllKv()
    {
        var list = typeof(CacheKeys).GetFields().Select(a => new
            {v = a.GetValue(a), k = a.GetCustomAttribute<DescriptionAttribute>()?.Description}).Where(a => a.v != null);
        return list.ToDictionary(a => a.k!, a => a.v?.ToString());
    }
}