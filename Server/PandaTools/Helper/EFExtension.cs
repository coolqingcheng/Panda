﻿using System.Linq.Expressions;

namespace PandaTools.Helper;

public static class EFExtension
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> exp) where T : class
    {
        if (condition)
        {
            query = query.Where(exp);
        }

        return query;
    }
}