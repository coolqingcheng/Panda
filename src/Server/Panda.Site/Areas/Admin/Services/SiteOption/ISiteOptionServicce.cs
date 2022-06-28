using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Panda.Entity.DataModels;
using System.Linq;

namespace Panda.Site.Areas.Admin.Services.SiteOption
{
    public interface ISiteOptionServicce
    {
        Task AddOrUpdate(Dictionary<string, string> dic);

        Task<int> GetInt(string key, int defaultValue);
        Task<string> GetString(string key, string defaultValue);

        Task<Dictionary<string, string>> GetAll();
    }
    public class SiteOptionServicce : ISiteOptionServicce
    {
        private readonly DbContext _context;

        private readonly IDistributedCache _cache;

        private const string SITEOPTION_CACHE_KEY = "SITEOPTION_CACHE_KEY";

        public SiteOptionServicce(DbContext context, IDistributedCache cache)
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
                    list.Add(new SiteOptions()
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
}
