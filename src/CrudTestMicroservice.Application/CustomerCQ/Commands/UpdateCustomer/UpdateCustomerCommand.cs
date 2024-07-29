namespace CrudTestMicroservice.Application.CustomerCQ.Commands.UpdateCustomer;
public record UpdateCustomerCommand : IRequest<Unit>
{
    //شناسه اصلی 
    public long Id { get; init; }
    //نام کوچک 
    public string FirstName { get; init; }
    //نام خانوادگی 
    public string LastName { get; init; }
    //تاریخ تولد 
    public DateTime DateOfBirth { get; init; }
    //شماره موبایل 
    public string PhoneNumber { get; init; }
    //ایمیل 
    public string Email { get; init; }
    //حساب بانکی 
    public string BankAccountNumber { get; init; }

}