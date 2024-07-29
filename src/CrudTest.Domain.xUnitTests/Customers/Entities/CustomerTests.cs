using CrudTest.Domain.xUnitTests.Builders;
using CrudTestMicroservice.Domain.Common.Exceptions;
using CrudTestMicroservice.Domain.Events;
using FluentAssertions;

namespace CrudTest.Domain.xUnitTests.Customers.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void GivenCustomer_WhenCreate_ThenCreate()
        {
            var cutomer = new CustomerBuilder().Build();
            cutomer.PhoneNumber.Should().NotBeNullOrWhiteSpace();
            cutomer.Email.Should().NotBeNullOrWhiteSpace();
            cutomer.DomainEvents.Where(e => e is CreateNewCustomerEvent).Should().HaveCount(1);
        }

        #region FirstNameAndLastName facts

        [Fact]
        public void GivenCustomer_WhenFirstNameIsEmpty_ThenError()
        {
            var cutomer = new CustomerBuilder().WithFirstName("");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Required input 'FirstName' is missing.");
        }

        [Fact]
        public void GivenCustomer_WhenFirstNameIsNull_ThenError()
        {
            var cutomer = new CustomerBuilder().WithFirstName(null);
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Required input 'FirstName' is missing.");
        }

        [Fact]
        public void GivenCustomer_WhenFirstNameIsWhiteSpace_ThenError()
        {
            var cutomer = new CustomerBuilder().WithFirstName("  ");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Required input 'FirstName' is missing.");
        }

        [Fact]
        public void GivenCustomer_WhenLastNameIsEmpty_ThenError()
        {
            var cutomer = new CustomerBuilder().WithLastName("");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Required input 'LastName' is missing.");
        }

        [Fact]
        public void GivenCustomer_WhenLastNameIsNull_ThenError()
        {
            var cutomer = new CustomerBuilder().WithLastName(null);
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Required input 'LastName' is missing.");
        }

        [Fact]
        public void GivenCustomer_WhenLastNameIsWhiteSpace_ThenError()
        {
            var cutomer = new CustomerBuilder().WithLastName("  ");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Required input 'LastName' is missing.");
        }

        #endregion


        #region PhoneNumber Facts

        [Fact]
        public void GivenCustomer_WhenPhoneNumberIsEmpty_ThenError()
        {
            var cutomer = new CustomerBuilder().WithPhoneNumber("");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid mobile number");
        }

        [Fact]
        public void GivenCustomer_WhenPhoneNumberIsNull_ThenError()
        {
            var cutomer = new CustomerBuilder().WithPhoneNumber(null);
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid mobile number");
        }

        [Fact]
        public void GivenCustomer_WhenPhoneNumberIsWhiteSpace_ThenError()
        {
            var cutomer = new CustomerBuilder().WithPhoneNumber("  ");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid mobile number");
        }

        [Fact]
        public void GivenCustomer_WhenPhoneNumberStartsWithInvalidNumber_ThenError()
        {
            var cutomer = new CustomerBuilder().WithPhoneNumber("69199877503");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid mobile number");
        }

        [Fact]
        public void GivenCustomer_WhenPhoneNumberByInvalidLength_ThenError()
        {
            var cutomer = new CustomerBuilder().WithPhoneNumber("091998775030");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid mobile number");
        }

        [Fact]
        public void GivenCustomer_WhenPhoneNumberIsNotNumber_ThenError()
        {
            var cutomer = new CustomerBuilder().WithPhoneNumber("0919987750a");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid mobile number");
        }

        #endregion


        #region Email Facts

        [Fact]
        public void GivenCustomer_WhenEmailIsEmpty_ThenError()
        {
            var cutomer = new CustomerBuilder().WithEmail("");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid email address");
        }

        [Fact]
        public void GivenCustomer_WhenEmailIsNull_ThenError()
        {
            var cutomer = new CustomerBuilder().WithEmail(null);
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid email address");
        }

        [Fact]
        public void GivenCustomer_WhenEmailIsWhiteSpace_ThenError()
        {
            var cutomer = new CustomerBuilder().WithEmail("  ");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid email address");
        }

        [Fact]
        public void GivenCustomer_WhenEmailWithoutAtSign_ThenError()
        {
            var cutomer = new CustomerBuilder().WithEmail("masoud.com");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid email address");
        }

        [Fact]
        public void GivenCustomer_WhenEmailInvalidStruct_ThenError()
        {
            var cutomer = new CustomerBuilder().WithEmail("masoud@gmail");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid email address");
        }

        #endregion
        
        #region BankAccountNumber Facts

        [Fact]
        public void GivenCustomer_WhenBankAccountNumberIsEmpty_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber("");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }

        [Fact]
        public void GivenCustomer_WhenBankAccountNumberIsNull_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber(null);
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }

        [Fact]
        public void GivenCustomer_WhenBankAccountNumberIsWhiteSpace_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber("  ");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }

        [Fact]
        public void GivenCustomer_WhenBankAccountNumberWithoutIR_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber("670690121201201762308001");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }

        [Fact]
        public void GivenCustomer_WhenBankAccountNumberStartsWithInvalidChar_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber("BB670690121201201762308001");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }
        [Fact]
        public void GivenCustomer_WhenBankAccountNumberGreaterThan26Digit_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber("IR6706901212012017623080011111");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }
        [Fact]
        public void GivenCustomer_WhenBankAccountNumberLessThan26Digit_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber("IR67069012120120176");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }
        [Fact]
        public void GivenCustomer_WhenBankAccountNumberInvalidContent_ThenError()
        {
            var cutomer = new CustomerBuilder().WithBankAccountNumber("IR670690121201201762308002");
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid IBAN");
        }
        #endregion
        
        #region DateOfBirth facts

        [Fact]
        public void GivenCustomer_WhenDateOfBirthIsMinValue_ThenError()
        {
            var cutomer = new CustomerBuilder().WithDateOfBirth(DateTime.MinValue);
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Invalid Date Of Birth");
        }

        [Fact]
        public void GivenCustomer_WhenDateOfBirthIsMaxValue_ThenError()
        {
            var cutomer = new CustomerBuilder().WithDateOfBirth(DateTime.MaxValue);
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Date of Birth cannot be in the future");
        }
        [Fact]
        public void GivenCustomer_WhenDateOfBirthIsInFuture_ThenError()
        {
            var cutomer = new CustomerBuilder().WithDateOfBirth(DateTime.Now.AddDays(1));
            Action action = () => cutomer.Build();
            action.Should().Throw<DomainException>().WithMessage("Date of Birth cannot be in the future");
        }
        #endregion
    }
}