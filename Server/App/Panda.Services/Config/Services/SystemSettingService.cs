using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;

namespace Panda.Services.Config.Services;

[Inject(InjectType.Scope)]
public class ConfigService
{
    private readonly DbContext _context;
    private readonly IDistributedCache _cache;
    public ConfigService(DbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<string> GetAsync(string key)
    {
        var cacheValue = await _cache.GetStringAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            cacheValue = await _context.Set<SysConfig>().Where(a => a.Key == key).Select(a => a.Value).FirstOrDefaultAsync();
            if (string.IsNullOrWhiteSpace(cacheValue) == false)
            {
                await _cache.SetStringAsync(key, cacheValue);
            }
        }
        return cacheValue!;
    }



    public async Task<string> GetAsync(string key, string defaultValue)
    {

        var cacheValue = await GetAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            return defaultValue;
        }
        return cacheValue;
    }



    public async Task<int> GetAsync(string key, int defaultValue)
    {
        var cacheValue = await GetAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            return defaultValue;
        }
        return Convert.ToInt32(cacheValue);
    }
    public async Task<bool> GetAsync(string key, bool defaultValue)
    {
        var cacheValue = await GetAsync(key);
        if (string.IsNullOrWhiteSpace(cacheValue))
        {
            return defaultValue;
        }
        return Convert.ToBoolean(cacheValue);
    }

    #region 获取Key，强类型
    public async Task<string> GetString<T>(Expression<Func<T, object>> exp, string defaultValue = "") where T : class, new()
    {
        var key = ConfigHelper.GetK<T>(exp);
        return await GetAsync(key, defaultValue);
    }
    public async Task<int> GetInt<T>(Expression<Func<T, object>> exp, int defaultValue = 0) where T : class, new()
    {
        var key = ConfigHelper.GetK<T>(exp);
        return await GetAsync(key, defaultValue);
    }

    public async Task<bool> GetBool<T>(Expression<Func<T, object>> exp, bool defaultValue = false) where T : class, new()
    {
        var key = ConfigHelper.GetK<T>(exp);
        return await GetAsync(key, defaultValue);
    }
    #endregion

    /// <summary>
    /// 更新配置
    /// </summary>
    /// <param name="dic"></param>
    /// <returns></returns>
    public async Task UpdateConfig(Dictionary<string, string> dic)
    {
        var list = await _context.Set<SysConfig>().ToListAsync();
        foreach (var (k, v) in dic)
        {
            var item = list.FirstOrDefault(a => a.Key == k);
            if (item == null)
            {
                _context.Set<SysConfig>().Add(new SysConfig { Key = k, Value = v });
                _cache.SetString($"{k}", v);
            }
            else
            {
                item.Value = v;
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task<T> Get<T>() where T : class, new()
    {
        var t = new T();


        foreach (var property in t.GetType().GetProperties())
        {
            var key = $"{t.GetType().Name}:{property.Name}";
            var value = await _cache.GetStringAsync(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                value = await _context.Set<SysConfig>().Where(a => a.Key == key).Select(a => a.Value).FirstOrDefaultAsync();
                if (value != null)
                {
                    await _cache.SetStringAsync(key, value);
                }
            }
            if (value != null)
            {
                property.SetValue(t, Convert.ChangeType(value, property.PropertyType));
            }
        }

        return t;
    }
}
