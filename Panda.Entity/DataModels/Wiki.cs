using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

/// <summary>
/// 文档表
/// </summary>
public class Wiki : PandaBaseTable
{
    /// <summary>
    /// 标题
    /// </summary>
    [StringLength(50)]
    public string Name { get; set; }

    /// <summary>
    /// 具体内容
    /// </summary>
    public virtual ICollection<WikiDoc> WikiDoc { get; set; }

    /// <summary>
    /// 分组
    /// </summary>
    public virtual ICollection<WikiGroup> WikiGroups { get; set; }
}

/// <summary>
/// 文档分组
/// </summary>
public class WikiGroup:PandaBaseTable
{
    [StringLength(20)] public string GroupName { get; set; }
}

/// <summary>
/// 文档名称
/// </summary>
public class WikiDoc : PandaBaseTable
{
    [DataType(DataType.Text)] public string WikiContent { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary>
    [StringLength(20)]
    public WikiGroup WikiGroup { get; set; }

    /// <summary>
    /// 所属文档
    /// </summary>
    public Wiki Wiki { get; set; }
}