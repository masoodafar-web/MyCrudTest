using CrudTestMicroservice.Domain.Events;
namespace CrudTestMicroservice.Application.CustomerCQ.Commands.UpdateCustomer;
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Customer), request.Id);
        request.Adapt(entity);
        _context.Customers.Update(entity);
        entity.AddDomainEvent(new UpdateCustomerEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
