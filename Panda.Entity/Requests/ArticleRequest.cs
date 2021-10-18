using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.Requests;

public class ArticleRequest
{
    
}

public class ArticleAddOrUpdate
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }
    
    [Required]
    public List<int> Categories { get; set; }
}

