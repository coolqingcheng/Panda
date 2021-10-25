using System.ComponentModel.DataAnnotations;

namespace Panda.Entity.DataModels;

public class PandaBaseTable
{
    [Key] public int Id { get; set; }

    
    /// <summary>
    /// 添加时间
    /// </summary>
    public DateTime AddTime { get; set; }


    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}