using Microsoft.AspNetCore.Mvc;
using Panda.Tools.Auth.Permission.Models;
using Panda.Tools.Auth.Permission.Scan;
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
    }

    public class PermissionUtils : IPermissionUtils
    {
        private readonly IPermissionScan _permissionScan;

        public PermissionUtils(IPermissionScan permissionScan)
        {
            _permissionScan = permissionScan;
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

        [HttpPost]
        public Task AccountSetPermission(AccountSetPermissionRequest request)
        {

        }
    }
}
