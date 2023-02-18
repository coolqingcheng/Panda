using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace QingCheng.Tools.EFCore;

public class TranContext
{
    private readonly DbContext _context;

    private IDbContextTransaction? _tran;

    private int _tranCount = 0;

    private readonly ILogger _logger;

    public TranContext(DbContext context, ILogger<TranContext> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task BeginTranAsync()
    {
        _tran ??= await _context.Database.BeginTransactionAsync();
        _tranCount += 1;
    }

    public async Task CommitTranAsync()
    {
        if (_tran == null)
        {
            throw new UserException("事务未开启");
        }

        _tranCount -= 1;
        if (_tranCount == 0)
        {
            await _tran?.CommitAsync();
        }
        else
        {
            _logger.LogInformation("事务计数:" + _tranCount);
        }
    }
}