namespace Panda.Site.Services.Post.Models;

public class NewestPostModel
{
    public string Link { get; set; }

    public string Title { get; set; }

    public DateTimeOffset UpdateTime { get; set; }
}