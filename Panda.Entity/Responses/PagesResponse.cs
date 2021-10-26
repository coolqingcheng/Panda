namespace Panda.Entity.Responses;

public class PagesResponse
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Url { get; set; }
    
    public string Content { get; set; }
}

public class PagesItem
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Url { get; set; }
    
    public DateTime AddTime { get; set; }
    
    public DateTime UpdateTime { get; set; }
}