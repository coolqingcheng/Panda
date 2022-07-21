using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Panda.Entity.DataModels;
using Panda.Tools.Attributes.Setting;
using Panda.Tools.Exception;
using System.Linq;
using System.Reflection;

namespace Panda.Site.Areas.Admin.Services.SiteOption
{
    public interface ISiteOptionService
    {
        Task AddOrUpdate(Dictionary<string, string> dic);
        Dictionary<string, string> GetDic<T>(T obj) where T : class, new();

        Task<T> GetModel<T>() where T : class, new();

        Task<int> GetInt(string key, int defaultValue);
        Task<string> GetString(string key, string defaultValue);

        Task<Dictionary<string, string>> GetAll();
    }

    public class SiteOptionService : ISiteOptionService
    {
        private readonly DbContext _context;

        private readonly IDistributedCache _cache;

        private const string SITEOPTION_CACHE_KEY = "SITEOPTION_CACHE_KEY";

        public SiteOptionService(DbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task AddOrUpdate(Dictionary<string, string> dic)
        {
            var list = await _context.Set<SiteOptions>().ToListAsync();
            foreach (var item in dic)
            {
                var optionItem = list.FirstOrDefault(a => a.Name == item.Key);
                if (optionItem == null)
                {
                    await _context.AddAsync(new SiteOptions()
                    {
                        Name = item.Key,
                        Value = item.Value
                    });
                }
                else
                {
                    optionItem.Value = item.Value;
                }
            }

            await _context.SaveChangesAsync();
            await _cache.RemoveAsync(SITEOPTION_CACHE_KEY);
        }

        public Dictionary<string, string> GetDic<T>(T obj) where T : class, new()
        {
            var dic = new Dictionary<string, string>();
            var type = obj.GetType();
            var prefixAttribute = type.GetCustomAttribute<SettingPrefixAttribute>();
            if (prefixAttribute==null)
            {
                throw new UserException($"{type.FullName}未设置配置前缀");
            }
            var prefix = prefixAttribute.Prefix;
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var value = propertyInfo.GetValue(obj);
                if (value != null)
                {
                    string key = $"{prefix}:{propertyInfo.Name}";
                    dic.TryAdd(key, value.ToString()!);
                }
            }

            return dic;
        }

        public async Task<T> GetModel<T>() where T : class, new()
        {
            var dic = await GetAll();
            var model = new T();
            var type = model.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                var value = dic.Where(a => a.Key == propertyInfo.Name)
                    .Select(a => a.Value).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(value) == false)
                {
                    model.SetValue(propertyInfo, value);
                }
            }

            return model;
        }

        public async Task<int> GetInt(string key, int defaultValue)
        {
            var item = await GetAsync(key);
            if (item == null)
            {
                return defaultValue;
            }

            return Convert.ToInt32(item);
        }

        private async Task<string?> GetAsync(string key)
        {
            var all = await GetAll();
            var item = all.Where(a => a.Key == key).Select(a => a.Value).FirstOrDefault();
            return item;
        }

        public async Task<Dictionary<string, string>> GetAll()
        {
            var list = await _context.Set<SiteOptions>().ToListAsync();
            var value = await _cache.GetStringAsync(SITEOPTION_CACHE_KEY);
            if (string.IsNullOrWhiteSpace(value) == false)
            {
                return value.JsonToObj<Dictionary<string, string>>();
            }

            var dic = list.ToDictionary(a => a.Name, a => a.Value);
            await _cache.SetStringAsync(SITEOPTION_CACHE_KEY, dic.ToJson());
            return dic;
        }

        public async Task<string> GetString(string key, string defaultValue)
        {
            var item = await GetAsync(key);
            if (item == null)
            {
                return defaultValue;
            }

            return item;
        }
    }


    public class SiteOptionAttribute : Attribute
    {
        public SiteOptionAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; set; }
    }
}