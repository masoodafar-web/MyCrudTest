using CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;

namespace CrudTestMicroservice.Application.Common.Mappings;

public class CustomerProfile : IRegister
{
    void IRegister.Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateNewCustomerCommand,Customer>()
            .Map(dest => dest, src => src).MapToConstructor(true);
    }
}
