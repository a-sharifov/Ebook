using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrasctrurcture.Core.Extensions;

public static class EntityFrameworkExtensions
{
    public static IQueryable<TEntity> Includes<TEntity>(
        this IQueryable<TEntity> query,
        params Expression<Func<TEntity, object>>[]? includes)
        where TEntity : class
    {
        if(includes == null || includes[0] == null)
        {
            return query;
        }

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
