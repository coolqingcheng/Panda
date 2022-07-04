using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Panda.Tools.Auth.Permission.Models;
using Panda.Tools.Auth.Permission.Scan;
using Panda.Tools.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Tools.Auth.Permission
{
    public interface IPermissionUtils
    {
        IEnumerable<PermissionGroup> GetAllPermission();
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="group"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        Task<bool> ChecPermission(Guid accountId, string group, string permissionName);
    }

    public class PermissionUtils : IPermissionUtils
    {
        private readonly IPermissionScan _permissionScan;


        private readonly IPermissionContainer _permissionContainer;


        public PermissionUtils(IPermissionScan permissionScan, IPermissionContainer permissionContainer)
        {
            _permissionScan = permissionScan;
            _permissionContainer = permissionContainer;
        }

        public async Task<bool> ChecPermission(Guid accountId, string group, string permissionName)
        {

            var list = await _permissionContainer.GetCachePermissions(accountId);
            return list.Contains($"{group}_{permissionName}");
        }

        public IEnumerable<PermissionGroup> GetAllPermission()
        {
            var dic = _permissionScan.GetAll();

            foreach (var (k, v) in dic)
            {
                var item = new PermissionGroup
                {
                    GroupName = k,
                    Key = k
                };
                item.List.AddRange(v.Select(a => new PermissionItem()
                {
                    Name = a,
                    Key = $"{k}-{a}"
                }));
                yield return item;
            }
        }
    }
}
