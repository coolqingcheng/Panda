using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Panda.Tools.Web;

namespace Panda.Tools.Helper;

public static class CacheHelper
{
    /// <summary>
    /// 缓存读取
    /// </summary>
    /// <param name="target"></param>
    /// <param name="key"></param>
    /// <param name="timeSpan"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T> CacheInvokeAsync<T>(Func<Task<T>> target, string key, TimeSpan timeSpan) where T : class
    {
        var cache = App.GetService<IDistributedCache>();
        var json = await cache.GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(json))
        {
            var item = await target.Invoke();
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(item));
            return item;
        }

        var data = JsonConvert.DeserializeObject<T>(json);
        return data;
    }
}