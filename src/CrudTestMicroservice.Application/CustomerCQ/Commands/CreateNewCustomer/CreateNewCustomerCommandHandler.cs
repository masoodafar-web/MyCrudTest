using CrudTestMicroservice.Domain.Events;

namespace CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;

public class CreateNewCustomerCommandHandler : IRequestHandler<CreateNewCustomerCommand, CreateNewCustomerResponseDto>
{
    private readonly IApplicationDbContext _context;

    public CreateNewCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateNewCustomerResponseDto> Handle(CreateNewCustomerCommand request,
        CancellationToken cancellationToken)
    {
        if ((await _context.Customers.AnyAsync(f => 
                (f.FirstName == request.FirstName && f.LastName == request.LastName && f.DateOfBirth.Date == request.DateOfBirth.Date)
                || f.Email == request.Email)))
        throw new DuplicateException("Customer Is Duplicate");


        var entity = request.Adapt<Customer>();
        await _context.Customers.AddAsync(entity, cancellationToken);
        // entity.AddDomainEvent(new CreateNewCustomerEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Adapt<CreateNewCustomerResponseDto>();
    }
}