using CrudTest.Domain.xUnitTests.Builders;
using CrudTestMicroservice.Application.CustomerCQ.Queries.GetAllCustomerByFilter;
using CrudTestMicroservice.Application.CustomerCQ.Queries.GetCustomer;
using FluentAssertions;

namespace CrudTest.Application.FunctionalTest.CustomerTest.Queries;

using static Testing;

public class GetCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnACustomer()
    {
        var newCustomerCommand = new CustomerBuilder().BuildCreateNewCommand();
        var createNewCustomerResponseDto = await SendAsync(newCustomerCommand);
        var query = new GetCustomerQuery()
        {
            Id = createNewCustomerResponseDto.Id
        };

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Id.Should().Be(createNewCustomerResponseDto.Id);
    }

    [Test]
    public async Task ShouldReturnAllByFilterCustomerLists()
    {

        var newCustomerCommand1 = new CustomerBuilder().WithFirstName("masoud1").WithLastName("moghaddam1").WithDateOfBirth(DateTime.Now.AddDays(-1)).WithEmail("masoud1@gmail.com").BuildCreateNewCommand();
        var createNewCustomerResponseDto1 = await SendAsync(newCustomerCommand1);
        
        var newCustomerCommand2 = new CustomerBuilder().WithFirstName("masoud2").WithLastName("moghaddam2").WithDateOfBirth(DateTime.Now.AddDays(-2)).WithEmail("masoud2@gmail.com").BuildCreateNewCommand();
        var createNewCustomerResponseDto2 = await SendAsync(newCustomerCommand2);
        
        var newCustomerCommand3 = new CustomerBuilder().WithFirstName("masoud3").WithLastName("moghaddam3").WithDateOfBirth(DateTime.Now.AddDays(-3)).WithEmail("masoud3@gmail.com").BuildCreateNewCommand();
        var createNewCustomerResponseDto3 = await SendAsync(newCustomerCommand3);
        
        var query = new GetAllCustomerByFilterQuery();

        var result = await SendAsync(query);

        result.Models.Should().HaveCountGreaterThanOrEqualTo(3);
    }

  
}
