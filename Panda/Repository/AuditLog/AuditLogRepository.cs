using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.AuditLog;

public class AuditLogRepository : PandaRepository<AuditLogs>
{
    public AuditLogRepository(PandaContext context) : base(context)
    {
    }
}