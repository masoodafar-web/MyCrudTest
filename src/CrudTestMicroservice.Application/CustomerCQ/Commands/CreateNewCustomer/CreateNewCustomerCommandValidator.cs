using CrudTestMicroservice.Domain.Validations;

namespace CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;
public class CreateNewCustomerCommandValidator : AbstractValidator<CreateNewCustomerCommand>
{
    private CustomerValidator _customerValidator=new ();
    public CreateNewCustomerCommandValidator()
    {
        RuleFor(model => model.FirstName)
            .NotEmpty().NotNull().WithMessage("Required input '{PropertyName}' is missing.");
        RuleFor(model => model.LastName)
            .NotEmpty().NotNull().WithMessage("Required input '{PropertyName}' is missing.");
        RuleFor(model => model.DateOfBirth)
            .NotNull().Must(m=>m<DateTime.Now).WithMessage("Date of Birth cannot be in the future").Must(m=> m>DateTime.MinValue).WithMessage("Invalid Date Of Birth");
        RuleFor(model => model.PhoneNumber)
            .NotEmpty().NotNull().Must(_customerValidator.BeAValidMobileNumber).WithMessage("Invalid mobile number");
        RuleFor(model => model.Email)
            .NotEmpty().NotNull().Must(_customerValidator.IsValidEmail).WithMessage("Invalid email address");
        RuleFor(model => model.BankAccountNumber)
            .NotEmpty().NotNull().Length(26).WithMessage("BankAccountNumber Must Be 26 Digit").Must(_customerValidator.BeAValidBankAccountNumber).WithMessage("Invalid IBAN");
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateNewCustomerCommand>.CreateWithOptions((CreateNewCustomerCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
