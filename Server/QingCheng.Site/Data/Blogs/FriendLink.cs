using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QingCheng.Tools.EFCore;

namespace QingCheng.Site.Data.Blogs
{
    public class FriendLink : BaseTableModel<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }
    }
}
