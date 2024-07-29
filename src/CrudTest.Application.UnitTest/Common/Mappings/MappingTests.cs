using System.Reflection;
using System.Runtime.CompilerServices;
using CrudTestMicroservice.Application.Common.Interfaces;
using CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Queries.GetAllCustomerByFilter;
using CrudTestMicroservice.Application.CustomerCQ.Queries.GetCustomer;
using CrudTestMicroservice.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace CrudTest.Application.UnitTest.Common.Mappings;

public class MappingTests
{
    private readonly IMapper _mapper;
    private readonly TypeAdapterConfig _config;
    public MappingTests()
    {
        _config = TypeAdapterConfig.GlobalSettings;
        _config.Scan(Assembly.GetAssembly(typeof(IApplicationDbContext)));

        _mapper = new Mapper(_config);
    }

    

    [Test]
    [TestCase(typeof(CreateNewCustomerCommand), typeof(Customer))]
    [TestCase(typeof(Customer), typeof(CreateNewCustomerCommand))]
    [TestCase(typeof(Customer), typeof(GetCustomerResponseDto))]
    [TestCase(typeof(List<Customer>), typeof(GetAllCustomerByFilterResponseModel))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        
        var instance = GetInstanceOf(source);
        if (source==typeof(CreateNewCustomerCommand))
        {
            instance = new CreateNewCustomerCommand()
            {
                FirstName = "masoud",
                LastName = "moghaddam",
                Email = "moghaddam@gmail.com",
                PhoneNumber = "09199877503",
                BankAccountNumber = "IR670690121201201762308001",
                DateOfBirth = DateTime.Now.AddDays(-1),
            };
        }
        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
