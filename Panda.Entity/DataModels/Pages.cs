using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Pages:PandaBaseTable
{
    
    [Required]
    public string UrlName { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
}