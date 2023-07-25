using PandaSite.Models;

namespace PandaSite.Api.Blogs.Services.Models
{
    public class FriendLinkModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public int Id { get; set; }

        public bool IsPublish { get; set; }
    }

    public class FriendLinkRequestModel :  BasePageModel
    {

    }
}
