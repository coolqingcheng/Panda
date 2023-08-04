using System.Linq.Expressions;
using PandaTools.Helper;

namespace Panda.Repositoies;

public class BaseRepository<T, TId> where T : BaseTableModel<TId>, new()
    where TId : struct
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

    public async Task<PageDto<T>> GetListAsync(Expression<Func<T, bool>> expression, int index, int size)
    {
        var query = DbContext.Set<T>().Where(expression);
        var list = await query.Page(index, size).OrderByDescending(a => a.Id).ToListAsync();
        var count = await query.CountAsync();
        return new PageDto<T>(count, list);
    }
}