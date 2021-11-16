using Panda.Entity;
using Panda.Entity.Requests;

namespace Panda.Services.AuditLog;

public interface IAuditLogService
{
    void Write(AuditLogRequest request);
}