using System.ComponentModel.DataAnnotations;

namespace Panda.Admin.Entities.DataModels;

public class PandaBaseTable : BaseTable
{
}

public class KeyGuidTable : PandaBaseTable
{
    [Key] public new Guid Id { get; set; }
}

public class BaseTable
{
    [Key] public virtual int Id { get; set; }


    /// <summary>
    ///     添加时间
    /// </summary>
    public DateTime AddTime { get; set; }

    /// <summary>
    ///     修改时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}