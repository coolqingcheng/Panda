using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Panda.Tools.Auth.Repositorys;
using Panda.Tools.Exception;

namespace Panda.Entity.Repositorys;

public class PandaRepository<T> : BaseRepository where T : PandaBaseTable
{
    protected new readonly PandaContext _context;

    protected PandaRepository(PandaContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().AsQueryable().ToListAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }


    public async Task RemoveAsync(T item)
    {
        _context.Set<T>().Remove(item);
        await _context.SaveChangesAsync();
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

    public async Task DeleteWhereAsync(Expression<Func<T, bool>> expression)
    {
        var list = await Where(expression).ToListAsync();
        _context.RemoveRange(list);
        await _context.SaveChangesAsync();
    }

    public async Task CheckExist(Expression<Func<T, bool>> expression, string message)
    {
        var exist = await Where(expression).AnyAsync();
        if (exist == false)
        {
            throw new UserException(message);
        }
    }

    public new IQueryable<T> Queryable()
    {
        return _context.Set<T>().AsQueryable();
    }
}