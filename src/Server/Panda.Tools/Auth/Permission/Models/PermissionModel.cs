using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Tools.Auth.Permission.Models
{
    public class PermissionGroup
    {
        public string GroupName { get; set; }

        public string Key { get; set; }

        public List<PermissionItem> List { get; set; } = new List<PermissionItem>();
    }

    public class PermissionItem
    {
        public string Name { get; set; }

        public string Key { get; set; }

        /// <summary>
        /// 是否授权给当前用户
        /// </summary>
        public bool IsGrant { get; set; }
    }
}
