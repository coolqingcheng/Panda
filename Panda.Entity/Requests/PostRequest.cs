using System.ComponentModel.DataAnnotations;
using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class PostRequest
{
    
}

public class PostAddOrUpdate
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }
    
    [Required]
    public List<int> Categories { get; set; }
}

public class AdminPostGetListRequest:BasePageRequest
{
    public string? KeyWord { get; set; }
}
