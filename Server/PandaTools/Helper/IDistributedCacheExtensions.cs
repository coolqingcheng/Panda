using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace PandaTools.Helper
{
    public static class IDistributedCacheExtensions
    {
        public static async Task<TModel?> GetModelAsync<TModel>(this IDistributedCache cache, string key) where TModel : class
        {
            try
            {
                var res = await cache.GetStringAsync(key);
                if (res == null)
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<TModel>(res);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async Task<TModel?> GetModelAsync<TModel>(this IDistributedCache cache, string key, Func<Task<TModel>> func, DistributedCacheEntryOptions? options = default) where TModel : class
        {
            try
            {
                var res = await cache.GetStringAsync(key);
                if (res == null)
                {
                    var model = await func();
                    await cache.SetModelAsync(key, model, options);
                    return model;
                }
                return JsonConvert.DeserializeObject<TModel>(res);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async Task SetModelAsync<TModel>(this IDistributedCache cache, string key, TModel value, DistributedCacheEntryOptions? options = default, CancellationToken token = default) where TModel : class
        {
            try
            {
                var json = JsonConvert.SerializeObject(value);
                await cache.SetStringAsync(key, json, options ?? new DistributedCacheEntryOptions(), token: token);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取cache配置
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="isSliding">是否是滑动过期。默认不是</param>
        /// <returns></returns>
        public static DistributedCacheEntryOptions GetOption(this IDistributedCache cache, TimeSpan timeSpan, bool isSliding = false)
        {
            return new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = isSliding == false ? timeSpan : null,
                SlidingExpiration = isSliding ? timeSpan : null
            };
        }
    }
}
