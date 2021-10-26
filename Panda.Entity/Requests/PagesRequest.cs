using System.ComponentModel.DataAnnotations;
using Panda.Tools.Models;

namespace Panda.Entity.Requests;

public class PagesRequest
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }
    [Required]
    public string Url { get; set; }
    [Required]
    public string Content { get; set; }
}

public class GetPagesRequest : BasePageRequest
{
    
}