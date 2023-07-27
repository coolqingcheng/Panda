namespace Panda.Models.Data.Blogs;

public class PostVisitRecord : BaseTimeTable
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public Posts Post { get; set; }

    public string IP { get; set; }

    public string UA { get; set; }

    public string UId { get; set; }
}