using System.Linq.Expressions;

namespace Panda.Repositoies;

public class BaseRepository<T> where T : class, new()
{
    protected readonly DbContext DbContext;

    public BaseRepository(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<bool> AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        var res = await DbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> AddAsync(IEnumerable<T> entitys)
    {
        await DbContext.Set<T>().AddRangeAsync(entitys);
        var res = await DbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);
        var res = await DbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        var res = await DbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAsync(List<T> entity)
    {
        DbContext.Set<T>().RemoveRange(entity);
        var res = await DbContext.SaveChangesAsync();
        return res > 0;
    }

    public async Task<T?> FindByIdAsync(object id)
    {
        var entity = await DbContext.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<List<T>> FindListAsync(Expression<Func<T, bool>> expression)
    {
        return await DbContext.Set<T>().Where(expression).ToListAsync();
    }
}