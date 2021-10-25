using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class Dictionaries:PandaBaseTable
{
    /// <summary>
    /// 分组
    /// </summary>
    [Required]
    public string GroupName { get; set; }

    /// <summary>
    /// 字典Key
    /// </summary>
    [Required]
    public string DicKey { get; set; }


    /// <summary>
    /// 字典Value
    /// </summary>
    public string DicValue { get; set; }
    
    
}