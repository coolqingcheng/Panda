using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Models
{
    public class RankListModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }


        public List<RankListItem> RankList { get; set; } = new();
    }

    public class RankListItem
    {
        public string Url { get; set; }

        public string Text { get; set; }
    }
}
