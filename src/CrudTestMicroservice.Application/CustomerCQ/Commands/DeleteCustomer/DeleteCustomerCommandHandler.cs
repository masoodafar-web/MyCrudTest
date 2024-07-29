using CrudTestMicroservice.Domain.Events;
namespace CrudTestMicroservice.Application.CustomerCQ.Commands.DeleteCustomer;
public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Customer), request.Id);
        entity.IsDeleted = true;
        _context.Customers.Update(entity);
        entity.AddDomainEvent(new DeleteCustomerEvent(entity));
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
