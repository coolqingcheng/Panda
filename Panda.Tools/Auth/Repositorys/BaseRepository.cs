using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Panda.Admin.Entities.DataModels;

namespace Panda.Tools.Auth.Repositorys;

public class BaseRepository
{
    protected readonly DbContext _context;

    public BaseRepository(DbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Where<T>(Expression<Func<T, bool>> expression) where T : PandaBaseTable
    {
        return _context.Set<T>().Where(expression);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IQueryable<T> Queryable<T>() where T : class,new()
    {
        return _context.Set<T>().AsQueryable();
    }
}