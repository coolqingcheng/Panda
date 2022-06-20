using Panda.Entity.Enums;

namespace Panda.Entity.Requests;

public class AuditLogRequest
{
    public string Message { get; set; }

    public string Stack { get; set; }

    public string AccountId { get; set; }

    public AuditLogType Type { get; set; }
}