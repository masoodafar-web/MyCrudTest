using FluentValidation;
using CrudTestMicroservice.Protobuf.Protos.Customer;
namespace CrudTestMicroservice.Protobuf.Validator.Customer;

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(model => model.Id)
            .NotNull();
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
        var result = await ValidateAsync(ValidationContext<UpdateCustomerRequest>.CreateWithOptions((UpdateCustomerRequest)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
