using System.Linq.Expressions;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository;

public class PandaRepository<T> where T:PandaBaseTable
{
    protected readonly PandaContext _context;

    protected PandaRepository(PandaContext context)
    {
        _context = context;
    }
}