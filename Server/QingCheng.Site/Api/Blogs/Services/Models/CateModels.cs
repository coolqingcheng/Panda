using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Api.Blogs.Services.Models
{
    internal class CateModels
    {
        
    }

    public class CateDtoModel
    {
        public int Id { get; set; }

        public string CateName { get; set; }

        public int PostCount { get; set; }

        public DateTime CreateTime { get; set; }


        public DateTime LastUpdateTime { get; set; }
    }

    public class CateRequest
    {
        public int Id { get; set; }

        public string CateName { get; set; }
    }
}
