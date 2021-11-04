using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class DicDatas : PandaBaseTable
{
    /// <summary>
    /// 字典Key
    /// </summary>
    [Required, StringLength(50)]
    public string DicKey { get; set; }

    /// <summary>
    /// 字典Value
    /// </summary>
    public string DicValue { get; set; }


    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 上级
    /// </summary>
    public int Pid { get; set; }
}