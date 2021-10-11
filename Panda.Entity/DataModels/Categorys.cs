using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Categorys:PandaBaseTable
{

    /// <summary>
    ///  名称
    /// </summary>
    [StringLength(20)]
    public string categoryName { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string CategoryDesc { get; set; }

    
    public List<ArticleCategoryRelations> ArticleCategoryRelations { get; set; }
}