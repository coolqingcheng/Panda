using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Services.Config
{
    public class SiteConfig
    {
        [Required]
        public string SiteName { get; set; }

        [Required]
        public string ICP { get; set; }

        /// <summary>
        /// 站点描述
        /// </summary>
        public string SiteDesc { get; set; }
    }
}
