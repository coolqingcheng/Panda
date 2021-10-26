using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository.Page;

public class PageRepository:PandaRepository<Pages>
{
    public PageRepository(PandaContext context) : base(context)
    {
    }
}