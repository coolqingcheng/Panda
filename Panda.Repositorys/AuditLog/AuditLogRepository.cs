using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.AuditLog;

public class AuditLogRepository:PandaRepository<AuditLogs>
{
    protected AuditLogRepository(PandaContext context) : base(context)
    {
    }

    public async Task AddAsync(AuditLogs entity)
    {
        await _context.AuditLogs.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}