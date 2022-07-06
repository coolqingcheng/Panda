using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Admin.Models.Accounts
{
    public class AccountPermissionModel
    {
        public bool IsAdmin { get; set; }

        public List<string> Permissions { get; set; } = new List<string>();
    }
}
