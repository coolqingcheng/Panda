using System;
using IP2Region;
using IP2Region.Models;

namespace Panda.Tools.Helper
{
	public class IpHelper : IDisposable
	{
        private DbSearcher _searcher;

        public IpHelper()
        {
            _searcher = new DbSearcher(Path.Combine(Environment.CurrentDirectory, "IpDb", "ip2region.db"));
        }

        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public string GetRegion(string ip)
        {
            try
            {
                var region =  _searcher.BinarySearch(ip).Region;

                region = string.Join("|",region.Split("|").Where(a => a != "0"));
                return region;

            }
            catch (System.Exception)
            {
                return "-";
            }
        }
       

        public void Dispose()
        {
            _searcher.Dispose();
        }
    }
}

