using FluentValidation;
using CrudTestMicroservice.Protobuf.Protos.Customer;
namespace CrudTestMicroservice.Protobuf.Validator.Customer;

public class CreateNewCustomerRequestValidator : AbstractValidator<CreateNewCustomerRequest>
{
    public CreateNewCustomerRequestValidator()
    {
        RuleFor(model => model.FirstName)
            .NotEmpty();
        RuleFor(model => model.LastName)
            .NotEmpty();
        RuleFor(model => model.DateOfBirth)
            .NotNull();
        RuleFor(model => model.PhoneNumber)
            .NotEmpty();
        RuleFor(model => model.Email)
            .NotEmpty();
        RuleFor(model => model.BankAccountNumber)
            .NotEmpty();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateNewCustomerRequest>.CreateWithOptions((CreateNewCustomerRequest)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
