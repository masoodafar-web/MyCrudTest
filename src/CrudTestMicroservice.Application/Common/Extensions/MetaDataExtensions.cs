namespace CrudTestMicroservice.Application.Common.Extensions;

public static class MetaDataExtensions
{
    public static async Task<MetaData> GetMetaData<T>(this IQueryable<T> source, PaginationState? paginationState,
        CancellationToken cancellationToken)
    {
        if (paginationState is null)
            return new MetaData
            {
                TotalCount = await source.CountAsync(cancellationToken)
            };

        var pageSize = paginationState.PageSize > 0 ? paginationState.PageSize : PaginationDefaults.PageSize;
        var pageNumber = paginationState.PageNumber > 0 ? paginationState.PageNumber : PaginationDefaults.PageNumber;
        var totalCount = await source.CountAsync(cancellationToken);
        var totalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);

        var metaData = new MetaData
        {
            CurrentPage = pageNumber,
            HasNext = pageNumber < totalPageCount,
            HasPrevious = pageNumber > 1,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPage = totalPageCount
        };
        return metaData;
    }
}
