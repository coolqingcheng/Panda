using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Tools.Auth.Permission
{
    public interface IPermissionContainer
    {
        /// <summary>
        /// 缓存用户权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="permissions">k:group v:permissionName</param>
        /// <returns></returns>
        Task SetCachePermissions(Guid accountId, Dictionary<string, string> permissions);

        Task<HashSet<string>> GetCachePermissions(Guid accountId);
    }

    public class PermissionContainer : IPermissionContainer
    {
        private readonly IDistributedCache _cache;

        private const string cache_key = "persssmion_{0}";

        public PermissionContainer(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCachePermissions(Guid accountId, Dictionary<string, string> permissions)
        {
            var set = new HashSet<string>();
            foreach (var item in permissions)
            {
                set.Add($"{item.Key}_{item.Value}");
            }
            await _cache.RemoveAsync(String.Format(cache_key, accountId));
            await _cache.SetStringAsync(String.Format(cache_key, accountId), set.ToJson());
        }

        public async Task<HashSet<string>> GetCachePermissions(Guid accountId)
        {
            var cache = await _cache.GetStringAsync(String.Format(cache_key, accountId));
            if (cache == null)
            {
                return new HashSet<string>();
            }
            var permissionList = cache.JsonToObj<HashSet<string>>();
            if (permissionList == null)
            {
                return new HashSet<string>();
            }
            return permissionList;
        }
    }
}
