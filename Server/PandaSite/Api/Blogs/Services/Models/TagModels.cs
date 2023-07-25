using PandaSite.Models;

namespace PandaSite.Api.Blogs.Services.Models
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
