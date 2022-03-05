using Mapster;
using Microsoft.EntityFrameworkCore;
using Panda.Entity.Requests;
using Panda.Repository.Account;
using Panda.Repository.Roles;

namespace Panda.Service.Roles;

public interface IRoleService
{
    Task EditRole(AddRoleReq roleReq);


    Task AddAccountRoleRelation(Guid accountId, Guid roleId);
}

public class RoleService : IRoleService
{
    private readonly RoleRepository _roleRepository;

    private readonly AccountRoleRepository _accountRoleRepository;

    private readonly AccountRepository _accountRepository;

    public RoleService(RoleRepository roleRepository, AccountRoleRepository accountRoleRepository,
        AccountRepository accountRepository)
    {
        _roleRepository = roleRepository;
        _accountRoleRepository = accountRoleRepository;
        _accountRepository = accountRepository;
    }

    public async Task EditRole(AddRoleReq roleReq)
    {
        if (roleReq.Id == null)
        {
            var role = roleReq.Adapt<Entity.DataModels.Roles>();
            await _roleRepository.AddAsync(role);
        }
        else
        {
            var role = await _roleRepository.Where(a => a.Id == roleReq.Id).FirstOrDefaultAsync();
            if (role != null)
            {
                roleReq.Adapt(role);
                await _roleRepository.SaveAsync();
            }
        }
    }

    public async Task AddAccountRoleRelation(Guid accountId, Guid roleId)
    {
        var account = await _accountRepository.Where(a => a.Id == accountId).FirstOrDefaultAsync();
        var role = await _roleRepository.Where(a => a.Id == roleId).FirstOrDefaultAsync();
        var accountRole = await _accountRoleRepository.Where(a => a.Account.Id == accountId && a.Role.Id == roleId)
            .FirstOrDefaultAsync();
        if (accountRole != null && account != null && role != null)
        {
            await _accountRoleRepository.AddRoleRelation(account, role);
        }
    }
}