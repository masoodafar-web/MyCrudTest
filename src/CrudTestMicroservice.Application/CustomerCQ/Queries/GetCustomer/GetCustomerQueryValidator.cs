namespace CrudTestMicroservice.Application.CustomerCQ.Queries.GetCustomer;
public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(model => model.Id)
            .NotNull();
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<GetCustomerQuery>.CreateWithOptions((GetCustomerQuery)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
