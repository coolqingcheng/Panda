using FreeSql.DataAnnotations;
using Panda.Entitys.Enums;

namespace Panda.Entity.DataModels;

public class AuditLog : PandaBaseTable
{
    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 堆栈
    /// </summary>
    public string Stack { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [Column(MapType = typeof(int))]
    public AuditType AuditType { get; set; }
    
    
}