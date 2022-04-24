using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Categorys : PandaBaseTable
{
    /// <summary>
    ///     名称
    /// </summary>
    [StringLength(20)]
    public string CategoryName { get; set; }

    /// <summary>
    ///     说明
    /// </summary>
    public string? CategoryDesc { get; set; }

    /// <summary>
    ///     是否显示
    /// </summary>
    public bool IsShow { get; set; }

    /// <summary>
    ///     上级Id
    /// </summary>
    public int Pid { get; set; }

    /// <summary>
    ///     关联总数
    /// </summary>
    public int Count { get; set; }


    public virtual List<PostsCategoryRelations> ArticleCategoryRelations { get; set; }
}