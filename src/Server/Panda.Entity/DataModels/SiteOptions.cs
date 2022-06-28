using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Entity.DataModels
{
    /// <summary>
    /// 站点配置
    /// </summary>
    public class SiteOptions
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
