using CrudTest.Domain.xUnitTests.Builders;
using CrudTestMicroservice.Application.Common.Exceptions;
using CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;
using CrudTestMicroservice.Domain.Entities;
using FluentAssertions;
using static CrudTest.Application.FunctionalTest.Testing;

namespace CrudTest.Application.FunctionalTest.CustomerTest.Commands;

public class CreateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateNewCustomerCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCustomer()
    {
        var newCustomerCommand = new CustomerBuilder().BuildCreateNewCommand();
        var createNewCustomerResponseDto = await SendAsync(newCustomerCommand);

        var item = await FindAsync<Customer>(createNewCustomerResponseDto.Id);

        item.Should().NotBeNull();
        item.Id.Should().BeGreaterThan(0);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }

    [Test]
    public async Task GivenCustomer_WhenCreateNewCustomerByDuplicateEmail_ThenDuplicateException()
    {
        var newCustomerCommand = new CustomerBuilder().BuildCreateNewCommand();
        var createNewCustomerResponseDto = await SendAsync(newCustomerCommand);
        await FluentActions.Invoking(() => SendAsync(newCustomerCommand)).Should().ThrowAsync<DuplicateException>().WithMessage("Customer Is Duplicate");
    }

    [Test]
    public async Task GivenCustomer_WhenCreateNewCustomerByDuplicateFirstNameAndLastNameAndDateOfBirth_ThenDuplicateException()
    {
        var newCustomerCommand = new CustomerBuilder().BuildCreateNewCommand();
        var createNewCustomerResponseDto = await SendAsync(newCustomerCommand);
        var newCustomerCommand2 = new CustomerBuilder().WithEmail("masoud@gamil.com").BuildCreateNewCommand();

        await FluentActions.Invoking(() => SendAsync(newCustomerCommand2)).Should().ThrowAsync<DuplicateException>().WithMessage("Customer Is Duplicate");
    }

    [Test]
    public async Task GivenCustomer_WhenCreateNewCustomerByInvalidEmail_ThenError()
    {
        try
        {
            var newCustomerCommand = new CustomerBuilder().WithEmail("masoud.com").BuildCreateNewCommand();
            var createNewCustomerResponseDto = await SendAsync(newCustomerCommand);
            await SendAsync(newCustomerCommand);
        }
        catch (ValidationException e)
        {
            e.Message.Should().Be("Invalid email address");
        }
    }
}