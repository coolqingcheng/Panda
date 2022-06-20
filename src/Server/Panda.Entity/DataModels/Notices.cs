using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Notices : PandaBaseTable
{
    [Required] public string Title { get; set; }

    [Required] public string Content { get; set; }

    /// <summary>
    ///     置顶
    /// </summary>
    public bool IsTop { get; set; }
}