using Panda.Admin.Entities.DataModels;
using Panda.Entity.Enums;

namespace Panda.Entity.DataModels;

public class AuditLogs : PandaBaseTable
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
    public AuditType AuditType { get; set; }
    
    
}