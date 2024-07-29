using CrudTestMicroservice.Domain.Entities;

namespace CrudTest.Domain.xUnitTests.Builders
{
    public class CustomerBuilder
    {
        private string _firstName = "masoud";
        private string _lastName = "moghaddam";
        private DateTime _dateOfBirth = DateTime.Now.AddDays(-1);
        private string _phoneNumber = "09199877503";
        private string _email = "masoud.afarin.moghaddam@gmail.com";
        private string _bankAccountNumber = "IR670690121201201762308001";

        public Customer Build()
        {
            return new Customer(_firstName, _lastName, _dateOfBirth, _phoneNumber, _email, _bankAccountNumber);
        }

        public CustomerBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public CustomerBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public CustomerBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public CustomerBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CustomerBuilder WithDateOfBirth(DateTime dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
            return this;
        }

        public CustomerBuilder WithBankAccountNumber(string bankAccountNumber)
        {
            _bankAccountNumber = bankAccountNumber;
            return this;
        }
    }
}