using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace Panda.Tools.Web.RazorPages
{
    public interface IStatisticUtils
    {
        /// <summary>
        /// 保存统计数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        Task Save(StatisticModel model);
    }

    public class StatisticModel
    {
        public string UId { get; set; }
        public string Ip { get; set; }

        public string Url { get; set; }

        public string UA { get; set; }

        public string Referer { get; set; }

        public ClientInfo Info { get; set; }
    }
}
