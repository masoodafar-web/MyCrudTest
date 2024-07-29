namespace CrudTestMicroservice.Application.CustomerCQ.Queries.GetAllCustomerByFilter;
public class GetAllCustomerByFilterQueryValidator : AbstractValidator<GetAllCustomerByFilterQuery>
{
    public GetAllCustomerByFilterQueryValidator()
    {
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<GetAllCustomerByFilterQuery>.CreateWithOptions((GetAllCustomerByFilterQuery)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
