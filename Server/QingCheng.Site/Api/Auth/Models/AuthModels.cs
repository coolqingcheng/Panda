using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Api.Auth.Models
{
    internal class AuthModels
    {
    }

    public class ChangeCurrPwd
    {
        public string OldPwd { get; set; }

        public string NewPwd { get; set; }
    }
}
