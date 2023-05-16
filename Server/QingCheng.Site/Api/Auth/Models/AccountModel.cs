using QingCheng.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Api.Auth.Models
{
    internal class AccountModel
    {
    }

    public class AccountListRequest : BasePageModel
    {

    }

    public class AccountItemDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public List<string> RoleName { get; set; } = new();

        public DateTime LockedTime { get; set; }

        public bool IsLocked
        {
            get
            {

                return LockedTime > DateTime.Now;

            }
        }
    }
}
