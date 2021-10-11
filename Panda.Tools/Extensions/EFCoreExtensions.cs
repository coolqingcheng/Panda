namespace Panda.Tools.Extensions;

public static class EFCoreExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int index, int size) where T : class,new()
    {
        if (index < 1)
        {
            index = 1;
        }

        return query.Skip((index - 1) * size).Take(size);
    }
}