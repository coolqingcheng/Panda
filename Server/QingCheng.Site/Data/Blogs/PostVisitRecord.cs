using QingCheng.Tools.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Data.Blogs
{
    public class PostVisitRecord : BaseTimeTable
    {

        public int Id { get; set; }

        public int PostId { get; set; }

        public Posts Post { get; set; }

        public string IP { get; set; }

        public string UA { get; set; }

        public string UId { get; set; }
    }
}
