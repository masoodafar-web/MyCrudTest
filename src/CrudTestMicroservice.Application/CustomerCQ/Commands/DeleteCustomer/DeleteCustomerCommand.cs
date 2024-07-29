namespace CrudTestMicroservice.Application.CustomerCQ.Commands.DeleteCustomer;
public record DeleteCustomerCommand : IRequest<Unit>
{
    //شناسه اصلی 
    public long Id { get; init; }

}