namespace CrudTestMicroservice.Application.CustomerCQ.Queries.GetCustomer;
public record GetCustomerQuery : IRequest<GetCustomerResponseDto>
{
    //شناسه اصلی 
    public long Id { get; init; }

}