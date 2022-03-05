using Panda.Entity;

namespace Panda.Repository.Roles;

public class RoleRepository : PandaRepository<Entity.DataModels.Roles>
{
    public RoleRepository(PandaContext context) : base(context)
    {
    }
}