namespace CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;
public record CreateNewCustomerCommand : IRequest<CreateNewCustomerResponseDto>
{
    // public CreateNewCustomerCommand()
    // {
    //     FirstName = "masoud";
    //     LastName = "moghaddam";
    //     Email= "moghaddam@gmail.com";
    //     PhoneNumber = "09199877503";
    //     BankAccountNumber = "IR670690121201201762308001";
    //     DateOfBirth=DateTime.Now.AddDays(-1);
    // }
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