using FluentValidation;
using CrudTestMicroservice.Protobuf.Protos.Customer;
namespace CrudTestMicroservice.Protobuf.Validator.Customer;

public class GetAllCustomerByFilterRequestValidator : AbstractValidator<GetAllCustomerByFilterRequest>
{
    public GetAllCustomerByFilterRequestValidator()
    {
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<GetAllCustomerByFilterRequest>.CreateWithOptions((GetAllCustomerByFilterRequest)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
