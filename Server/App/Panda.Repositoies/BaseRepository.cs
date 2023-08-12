using System.Linq.Expressions;
using PandaTools.Helper;

namespace Panda.Repositoies;

public abstract class BaseRepository<T, TId> where T : BaseTableModel<TId>, new()
    where TId : struct
{
    protected readonly DbContext Ctx;

    public BaseRepository(DbContext ctx)
    {
        Ctx = ctx;
    }

    public async Task<bool> AddAsync(T entity)
    {
        await Ctx.Set<T>().AddAsync(entity);
        var res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> AddAsync(IEnumerable<T> entitys)
    {
        await Ctx.Set<T>().AddRangeAsync(entitys);
        var res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        Ctx.Set<T>().Update(entity);
        var res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        Ctx.Set<T>().Remove(entity);
        var res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAsync(List<T> entity)
    {
        Ctx.Set<T>().RemoveRange(entity);
        var res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<T?> FindByIdAsync(object id)
    {
        var entity = await Ctx.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<PageDto<T>> GetListAsync(Expression<Func<T, bool>> expression, int index, int size)
    {
        var query = Ctx.Set<T>().Where(expression);
        var list = await query.Page(index, size).OrderByDescending(a => a.Id).ToListAsync();
        var count = await query.CountAsync();
        return new PageDto<T>(count, list);
    }
}