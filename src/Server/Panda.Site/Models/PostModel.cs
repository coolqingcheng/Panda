namespace Panda.Site.Models;

public class PostModel
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Link { get; set; }

    public DateTimeOffset UpdateTime { get; set; }


    public string Author { get; set; }

    public string Content { get; set; }

    public List<PostCategory>? Categorys { get; set; }

    public NextPrePost? NextPost { get; set; }
    
    public NextPrePost? PrePost { get; set; }
}

public class PostCategory
{
    public string CateName { get; set; }

    public int CateId { get; set; }
}

public class NextPrePost
{
    public string Link { get; set; }

    public string Title { get; set; }
    
    
}

public class PostListItem
{
    public string Link { get; set; }

    
    public string Title { get; set; }

    public DateTimeOffset UpdateTime { get; set; }


    public string Author { get; set; }

    public string? Cover { get; set; }


    public string Summay { get; set; }
}