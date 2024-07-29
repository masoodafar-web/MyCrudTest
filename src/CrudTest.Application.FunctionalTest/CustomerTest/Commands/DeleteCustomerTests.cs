using CrudTest.Domain.xUnitTests.Builders;
using CrudTestMicroservice.Application.Common.Exceptions;
using CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Commands.DeleteCustomer;
using CrudTestMicroservice.Domain.Entities;
using FluentAssertions;
using static CrudTest.Application.FunctionalTest.Testing;

namespace CrudTest.Application.FunctionalTest.CustomerTest.Commands;

public class DeleteCustomerTests: BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCustomerId()
    {
        var command = new DeleteCustomerCommand()
        {
            Id = 20
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task ShouldDeleteCustomer()
    {
        var newCustomerCommand = new CustomerBuilder().BuildCreateNewCommand();
        var createNewCustomerResponseDto = await SendAsync(newCustomerCommand);

        await SendAsync(new DeleteCustomerCommand()
        {
            Id = createNewCustomerResponseDto.Id
        });
        var item = await FindAsync<Customer>(createNewCustomerResponseDto.Id);

        item.Should().BeNull();
    }
}