using QingCheng.Site.Api.Services.Models;
using QingCheng.Site.Data.Entitys;
using QingCheng.Site.Models;

namespace QingCheng.Site.Api.Services
{
    public class RoleService
    {
        private readonly DbContext _db;

        public RoleService(DbContext db)
        {
            _db = db;
        }

        public async Task AddRole(string roleName)
        {
            var any = await _db.Set<AccountRoles>().Where(a => a.RoleName == roleName).AnyAsync();
            if (any)
            {
                throw new UserException("已经存在");
            }

            await _db.Set<AccountRoles>().AddAsync(new AccountRoles()
            {
                RoleName = roleName,
            });
            await _db.SaveChangesAsync();
        }

        public async Task SetAccountToRole(Guid accountId, Guid roleId)
        {
            var account = await _db.Set<Accounts>().Where(a => a.Id == accountId).FirstOrDefaultAsync();
            var role = await _db.Set<AccountRoles>().FirstOrDefaultAsync(a => a.Id == roleId);
            if (role == null || account == null)
            {
                throw new UserException("操作失败！");
            }
            role.AccountRoleRelations.Add(new AccountRoleRelation()
            {
                Account = account,
                AccountRole = role
            });
            await _db.SaveChangesAsync();
        }

        public async Task DelRole(Guid id)
        {
            var role = await _db.Set<AccountRoles>().Include(a => a.AccountRoleRelations).Where(a => a.Id == id).Select(a => new
            {
                roleName = a.RoleName,
                count = a.AccountRoleRelations.Count()
            }).FirstOrDefaultAsync();
            if (role == null)
            {
                throw new UserException("删除失败");
            }

            if (role.count > 0)
            {
                throw new UserException("角色关联账户大于0");
            }

            await _db.Set<AccountRoles>().Where(a => a.Id == id).ExecuteDeleteAsync();
            await _db.SaveChangesAsync();
        }

        public async Task<PageDto<RoleListResponse>> GetRoleList(RoleListRequest request)
        {
            var query = _db.Set<AccountRoles>().AsQueryable();
            var list = await _db.Set<AccountRoles>().Skip(request.Skip).Take(request.PageSize)
                  .Select(a => new RoleListResponse()
                  {
                      Id = a.Id,
                      RoleName = a.RoleName,
                      Count = a.AccountRoleRelations.Count
                  }).ToListAsync();
            return new PageDto<RoleListResponse>(await query.CountAsync(), list);
        }


    }
}
