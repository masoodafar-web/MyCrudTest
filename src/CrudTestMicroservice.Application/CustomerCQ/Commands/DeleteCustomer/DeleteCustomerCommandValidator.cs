namespace CrudTestMicroservice.Application.CustomerCQ.Commands.DeleteCustomer;
public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(model => model.Id)
            .NotNull().GreaterThan(0).WithMessage("Id should be greater than zero");
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<DeleteCustomerCommand>.CreateWithOptions((DeleteCustomerCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
