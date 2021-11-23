using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Panda.Entity;
using Panda.Entity.DataModels;

namespace Panda.Repository;

public class PandaRepository<T> where T : PandaBaseTable
{
    protected readonly PandaContext _context;

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().AsQueryable().ToListAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public IQueryable<T> Queryable()
    {
        return _context.Set<T>().AsQueryable();
    }

    protected PandaRepository(PandaContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync()
    {
        return await _context.Set<T>().AnyAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWhere(Expression<Func<T, bool>> expression)
    {
        var list = await Where(expression).ToListAsync();
        if (list != null)
        {
            _context.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}