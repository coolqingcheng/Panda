using System.Linq.Expressions;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository;

public class PandaRepository<T> where T:PandaBaseTable
{
    protected readonly PandaContext _context;


    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    protected PandaRepository(PandaContext context)
    {
        _context = context;
    }
}