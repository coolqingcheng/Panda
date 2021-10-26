using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Pages:PandaBaseTable
{
    
    [Required]
    public string Url { get; set; }

    [Required]
    public string Title { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    [Required]
    public string Content { get; set; }
}