namespace CrudTestMicroservice.Application.CustomerCQ.Queries.GetAllCustomerByFilter;
public class GetAllCustomerByFilterResponseDto
{
    //متادیتا 
    public MetaData MetaData { get; set; }
    //مدل خروجی 
    public List<GetAllCustomerByFilterResponseModel>? Models { get; set; }

}public class GetAllCustomerByFilterResponseModel
{
    //شناسه اصلی 
    public long Id { get; set; }
    //نام کوچک 
    public string FirstName { get; set; }
    //نام خانوادگی 
    public string LastName { get; set; }
    //تاریخ تولد 
    public DateTime DateOfBirth { get; set; }
    //شماره موبایل 
    public string PhoneNumber { get; set; }
    //ایمیل 
    public string Email { get; set; }
    //حساب بانکی 
    public string BankAccountNumber { get; set; }
}
