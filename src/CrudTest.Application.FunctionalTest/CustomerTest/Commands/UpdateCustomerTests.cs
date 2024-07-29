using CrudTest.Domain.xUnitTests.Builders;
using CrudTestMicroservice.Application.Common.Exceptions;
using CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Commands.DeleteCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Commands.UpdateCustomer;
using CrudTestMicroservice.Domain.Entities;
using FluentAssertions;
using static CrudTest.Application.FunctionalTest.Testing;

namespace CrudTest.Application.FunctionalTest.CustomerTest.Commands;

public class UpdateCustomerTests: BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command =  new CustomerBuilder().BuildUpdateCustomerCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateCustomer()
    {
    
        var newCustomerCommand = new CustomerBuilder().BuildCreateNewCommand();
        var createNewCustomerResponseDto = await SendAsync(newCustomerCommand);
        
        var command =  new CustomerBuilder().WithId(createNewCustomerResponseDto.Id).WithFirstName("masoud1").BuildUpdateCustomerCommand();
        await SendAsync(command);
    
        var item = await FindAsync<Customer>(createNewCustomerResponseDto.Id);
    
        item.Should().NotBeNull();
        item!.Id.Should().Be(createNewCustomerResponseDto.Id);
        item.FirstName.Should().Be("masoud1");//updated
        item.LastName.Should().Be(newCustomerCommand.LastName);
        item.PhoneNumber.Should().Be(newCustomerCommand.PhoneNumber);
        item.Email.Should().Be(newCustomerCommand.Email);
        item.BankAccountNumber.Should().Be(newCustomerCommand.BankAccountNumber);
        item.DateOfBirth.Date.Should().Be(newCustomerCommand.DateOfBirth.Date);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}