using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Models
{
    internal class PostModel
    {
    }

    public class PostCateModel
    {
        public string CateName { get; set; }

        public int Id { get; set; }
    }

    public class PostTagModel
    {
        public int Id { get; set; }

        public string TagName { get; set; }
    }

    public class PostDetailModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }


        public List<PostTagModel> Tags { get; set; }


        public List<PostCateModel> Cates { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public int ReadCount { get; set; }
    }
}
