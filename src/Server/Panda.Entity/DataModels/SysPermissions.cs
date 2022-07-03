using Panda.Tools.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Entity.DataModels
{

    public class SysPermissions : BaseTable
    {
        public new long Id { get; set; }

        public Accounts? Account { get; set; }

        public Roles? Role { get; set; }


        public string PermissionKey { get; set; }
    }
}
