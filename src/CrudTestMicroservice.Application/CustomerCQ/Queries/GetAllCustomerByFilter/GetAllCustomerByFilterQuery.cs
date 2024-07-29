namespace CrudTestMicroservice.Application.CustomerCQ.Queries.GetAllCustomerByFilter;
public record GetAllCustomerByFilterQuery : IRequest<GetAllCustomerByFilterResponseDto>
{
    //موقعیت صفحه بندی 
    public PaginationState? PaginationState { get; init; }
    //مرتب سازی بر اساس 
    public string? SortBy { get; init; }
    //فیلتر 
    public GetAllCustomerByFilterFilter? Filter { get; init; }

}public class GetAllCustomerByFilterFilter
{
    //شناسه اصلی 
    public long? Id { get; set; }
    //نام کوچک 
    public string? FirstName { get; set; }
    //نام خانوادگی 
    public string? LastName { get; set; }
    //تاریخ تولد 
    public DateTime? DateOfBirth { get; set; }
    //شماره موبایل 
    public string? PhoneNumber { get; set; }
    //ایمیل 
    public string? Email { get; set; }
    //حساب بانکی 
    public string? BankAccountNumber { get; set; }
}
