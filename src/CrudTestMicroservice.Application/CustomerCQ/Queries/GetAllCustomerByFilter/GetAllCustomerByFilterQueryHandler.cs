namespace CrudTestMicroservice.Application.CustomerCQ.Queries.GetAllCustomerByFilter;
public class GetAllCustomerByFilterQueryHandler : IRequestHandler<GetAllCustomerByFilterQuery, GetAllCustomerByFilterResponseDto>
{
    private readonly IApplicationDbContext _context;

    public GetAllCustomerByFilterQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetAllCustomerByFilterResponseDto> Handle(GetAllCustomerByFilterQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Customers
            .ApplyOrder(sortBy: request.SortBy)
            .AsNoTracking()
            .AsQueryable();
        if (request.Filter is not null)
        {
            query = query
                .Where(x => request.Filter.Id == null || x.Id == request.Filter.Id)
                .Where(x => request.Filter.FirstName == null || x.FirstName.Contains(request.Filter.FirstName))
                .Where(x => request.Filter.LastName == null || x.LastName.Contains(request.Filter.LastName))
                .Where(x => request.Filter.DateOfBirth == null || x.DateOfBirth == request.Filter.DateOfBirth)
                .Where(x => request.Filter.PhoneNumber == null || x.PhoneNumber.Contains(request.Filter.PhoneNumber))
                .Where(x => request.Filter.Email == null || x.Email.Contains(request.Filter.Email))
                .Where(x => request.Filter.BankAccountNumber == null || x.BankAccountNumber.Contains(request.Filter.BankAccountNumber))
;
        }
        return new GetAllCustomerByFilterResponseDto
        {
            MetaData = await query.GetMetaData(request.PaginationState, cancellationToken),
            Models = await query.PaginatedListAsync(paginationState: request.PaginationState)
                .ProjectToType<GetAllCustomerByFilterResponseModel>().ToListAsync(cancellationToken)
        };        
    }
}
