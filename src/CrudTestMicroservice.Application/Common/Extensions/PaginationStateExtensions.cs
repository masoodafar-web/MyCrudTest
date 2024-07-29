namespace CrudTestMicroservice.Application.Common.Extensions;

public static class PaginationStateExtensions
{
    public static IQueryable<TSource> PaginatedListAsync<TSource>(this IQueryable<TSource> source,
        PaginationState? paginationState)
    {
        if (paginationState is null)
            return source;

        var pageSize = paginationState.PageSize > 0 ? paginationState.PageSize : PaginationDefaults.PageSize;
        var pageNumber = paginationState.PageNumber > 0 ? paginationState.PageNumber : PaginationDefaults.PageNumber;
        var paginationSkip = pageSize * (pageNumber - 1);

        return source.Skip(paginationSkip).Take(pageSize);
    }
}

public static class PaginationDefaults
{
    public const int PageSize = 10;
    public const int PageNumber = 1;
    public const int Skip = 0;
    public const int Take = PageSize;
}
