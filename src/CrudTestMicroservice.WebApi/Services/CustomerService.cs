using CrudTestMicroservice.Protobuf.Protos.Customer;
using CrudTestMicroservice.WebApi.Common.Services;
using CrudTestMicroservice.Application.CustomerCQ.Commands.CreateNewCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Commands.UpdateCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Commands.DeleteCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Queries.GetCustomer;
using CrudTestMicroservice.Application.CustomerCQ.Queries.GetAllCustomerByFilter;
namespace CrudTestMicroservice.WebApi.Services;
public class CustomerService : CustomerContract.CustomerContractBase
{
    private readonly IDispatchRequestToCQRS _dispatchRequestToCQRS;

    public CustomerService(IDispatchRequestToCQRS dispatchRequestToCQRS)
    {
        _dispatchRequestToCQRS = dispatchRequestToCQRS;
    }
    public override async Task<CreateNewCustomerResponse> CreateNewCustomer(CreateNewCustomerRequest request, ServerCallContext context)
    {
        return await _dispatchRequestToCQRS.Handle<CreateNewCustomerRequest, CreateNewCustomerCommand, CreateNewCustomerResponse>(request, context);
    }
    public override async Task<Empty> UpdateCustomer(UpdateCustomerRequest request, ServerCallContext context)
    {
        return await _dispatchRequestToCQRS.Handle<UpdateCustomerRequest, UpdateCustomerCommand>(request, context);
    }
    public override async Task<Empty> DeleteCustomer(DeleteCustomerRequest request, ServerCallContext context)
    {
        return await _dispatchRequestToCQRS.Handle<DeleteCustomerRequest, DeleteCustomerCommand>(request, context);
    }
    public override async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request, ServerCallContext context)
    {
        return await _dispatchRequestToCQRS.Handle<GetCustomerRequest, GetCustomerQuery, GetCustomerResponse>(request, context);
    }
    public override async Task<GetAllCustomerByFilterResponse> GetAllCustomerByFilter(GetAllCustomerByFilterRequest request, ServerCallContext context)
    {
        return await _dispatchRequestToCQRS.Handle<GetAllCustomerByFilterRequest, GetAllCustomerByFilterQuery, GetAllCustomerByFilterResponse>(request, context);
    }
}
