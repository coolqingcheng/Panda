using Microsoft.EntityFrameworkCore;
using Panda.Admin.Admin.Models.Permission;
using Panda.Entity.DataModels;
using Panda.Tools.Auth.Models;
using System.Linq;

namespace Panda.Admin.Services.Permission
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public interface IPermissionService
    {

        Task AccountSetPermission(AccountSetPermissionRequest request);

        Task<HashSet<string>> GetAccountPermission(Guid accountId);
    }

    public class PermissionService : IPermissionService
    {
        private readonly DbContext _context;

        public PermissionService(DbContext context)
        {
            _context = context;
        }

        public async Task AccountSetPermission(AccountSetPermissionRequest request)
        {
            var list = await _context.Set<SysPermissions>().Where(a => a.Account!.Id == request.AccountId).ToListAsync();
            var account = await _context.Set<Accounts>().Where(a => a.Id == request.AccountId).FirstOrDefaultAsync();
            using var tran = await _context.Database.BeginTransactionAsync();
            if (list.Count > 0)
            {
                _context.RemoveRange(list);
                await _context.SaveChangesAsync();
            }
            foreach (var item in request.PermissionKeys)
            {
                _context.Set<SysPermissions>().Add(new SysPermissions()
                {
                    Account = account,
                    Role = null,
                    PermissionKey = item
                });
            }
            await _context.SaveChangesAsync();
            await tran.CommitAsync();
        }

        public async Task<HashSet<string>> GetAccountPermission(Guid accountId)
        {
            var accountPermissionKeys = await _context.Set<SysPermissions>().Where(a => a.Account!.Id == accountId).Select(a => a.PermissionKey).ToListAsync();

            return accountPermissionKeys.ToHashSet();
        }
    }
}
