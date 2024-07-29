namespace CrudTestMicroservice.WebApi.Common.Mappings;

public class CustomerProfile : IRegister
{
    void IRegister.Register(TypeAdapterConfig config)
    {
        //config.NewConfig<Source,Destination>()
        //    .Map(dest => dest.FullName, src => $"{src.Firstname} {src.Lastname}");
    }
}
