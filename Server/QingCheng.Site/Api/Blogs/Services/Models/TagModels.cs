using QingCheng.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingCheng.Site.Api.Blogs.Services.Models
{
    internal class TagModels
    {
    }

    public class TagDtoModel
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public int Count { get; set; }
    }

    public class TagListRequest : BasePageModel
    {

    }
}
