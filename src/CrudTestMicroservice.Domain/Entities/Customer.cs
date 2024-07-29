using CrudTestMicroservice.Domain.Common.Exceptions;
using CrudTestMicroservice.Domain.Validations;

namespace CrudTestMicroservice.Domain.Entities;
public class Customer : BaseAuditableEntity
{
    //نام کوچک
    public string FirstName { get; private set;  }
    //نام خانوادگی
    public string LastName { get; private set;  }
    //تاریخ تولد
    public DateTime DateOfBirth { get; private set;  }
    //شماره موبایل
    public string PhoneNumber { get; private set;  }
    //ایمیل
    public string Email { get; private set;  }
    //حساب بانکی
    public string BankAccountNumber { get; private set;  }
    
    public Customer(string firstName, string lastName,DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
    {
        
        Create(firstName, lastName,dateOfBirth, phoneNumber, email, bankAccountNumber);
    }

    private CustomerValidator _customerValidator = new();
    public Customer Create(string firstName, string lastName,DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
    {
       
        _customerValidator.Validate(firstName, lastName,dateOfBirth, phoneNumber, email, bankAccountNumber);
        // validation should go here before the aggregate is created
        // an aggregate should never be in an invalid state
        // the temperature is validated in the Temperature ValueObject and is always valid
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        Email = email;
        BankAccountNumber = bankAccountNumber;
        
        PublishCreated();
        return this;
    }
    
    private void PublishCreated()
    {
        AddDomainEvent(new CreateNewCustomerEvent(this));
    }
   
}
