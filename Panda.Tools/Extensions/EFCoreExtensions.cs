using System.Linq.Expressions;
using Panda.Tools.Models;

namespace Panda.Tools.Extensions;

public static class EFCoreExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int index, int size) where T : class, new()
    {
        if (index < 1)
        {
            index = 1;
        }

        return query.Skip((index - 1) * size).Take(size);
    }

    public static IQueryable<T> Page<T>(this IQueryable<T> query, BasePageRequest request) where T : class, new()
    {
        return query.Page<T>(request.Index, request.Size);
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool whereIf, Expression<Func<T, bool>> expression)
    {
        return whereIf ? query.Where(expression) : query;
    }
}