using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Categorys:PandaBaseTable
{

    /// <summary>
    ///  名称
    /// </summary>
    [Column(StringLength = 100)]
    public string categoryName { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string CategoryDesc { get; set; }
}