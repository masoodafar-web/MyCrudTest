namespace CrudTestMicroservice.Application.CustomerCQ.Queries.GetCustomer;
public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, GetCustomerResponseDto>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetCustomerResponseDto> Handle(GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _context.Customers
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectToType<GetCustomerResponseDto>()
            .FirstOrDefaultAsync(cancellationToken);

        return response ?? throw new NotFoundException(nameof(Customer), request.Id);
    }
}
