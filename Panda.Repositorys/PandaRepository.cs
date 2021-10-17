using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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
       await  _context.SaveChangesAsync();
    }

    public async Task DeleteOne(Expression<Func<T, bool>> expression)
    {
        var item = await Where(expression).FirstOrDefaultAsync();
        if (item!=null)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}