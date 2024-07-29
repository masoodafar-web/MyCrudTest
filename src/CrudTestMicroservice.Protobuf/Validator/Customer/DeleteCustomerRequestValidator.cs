using FluentValidation;
using CrudTestMicroservice.Protobuf.Protos.Customer;
namespace CrudTestMicroservice.Protobuf.Validator.Customer;

public class DeleteCustomerRequestValidator : AbstractValidator<DeleteCustomerRequest>
{
    public DeleteCustomerRequestValidator()
    {
        RuleFor(model => model.Id)
            .NotNull();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<DeleteCustomerRequest>.CreateWithOptions((DeleteCustomerRequest)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
