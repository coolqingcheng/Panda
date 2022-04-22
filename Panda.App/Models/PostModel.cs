namespace Panda.App.Models;

public class PostModel
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string CateName { get; set; }

    public int CateId { get; set; }

    public NextPrePost NextPost { get; set; }
    
    public NextPrePost PrePost { get; set; }
}

public class NextPrePost
{
    public string Id { get; set; }

    public string Title { get; set; }
    
    
}