using Microsoft.Extensions.DependencyInjection;
using Panda.Entity.DataModels;
using Panda.Entity.Requests;
using Panda.Repository.AuditLog;

namespace Panda.Services.AuditLog;

public class AuditLogService : IAuditLogService
{
    private readonly IServiceProvider _serviceProvider;

    public AuditLogService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public void Write(AuditLogRequest request)
    {
        _ = Task.Run(async () =>
        {
            using var scope = _serviceProvider.CreateScope();
            var auditLogRepository = scope.ServiceProvider.GetService<AuditLogRepository>();
            var item = new AuditLogs()
            {
                Message = request.Message
            };
            await auditLogRepository?.AddAsync(item)!;
        });
    }
}