using System.Linq.Dynamic.Core;
using CrudTestMicroservice.Domain.Common;

namespace CrudTestMicroservice.Application.Common.Extensions;

public static class SortByExtensions
{
    public static IQueryable<TSource> ApplyOrder<TSource>(this IQueryable<TSource> source,
        string? sortBy) where TSource : BaseAuditableEntity
    {
        // default sort approach
        if (sortBy is null or "")
        {
            source = source.OrderByDescending(p => p.Created);
            return source;
        }

        // sort using dynamic linq
        source = source.OrderBy(sortBy);

        return source;
    }
}
