using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panda.Entity.DataModels
{
    public class Notices : PandaBaseTable
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 置顶
        /// </summary>
        public bool IsTop { get; set; }
    }
}
