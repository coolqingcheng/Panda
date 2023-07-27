namespace Panda.Models.Dtos.Blogs.Posts;

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