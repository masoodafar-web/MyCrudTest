namespace CrudTestMicroservice.WebApi.Common.Services;
public interface IDispatchRequestToCQRS
{
    Task<TResponse> Handle<TRequest, TCommand, TResponse>(TRequest request,
        ServerCallContext context);
    Task<Empty> Handle<TRequest, TCommand>(TRequest request,
        ServerCallContext context);
    Task<TResponse> Handle<TCommand, TResponse>(ServerCallContext context);
}
public class DispatchRequestToCQRS : IDispatchRequestToCQRS
{
    private readonly ISender _sender;

    public DispatchRequestToCQRS(ISender sender)
    {
        _sender = sender;
    }

    public async Task<TResponse> Handle<TRequest, TCommand, TResponse>(TRequest request,
        ServerCallContext context)
    {
        try
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var cqrsInput = request.Adapt<TCommand>();

            if (cqrsInput is null)
            {
                throw new ArgumentNullException(nameof(cqrsInput));
            }

            var output = await _sender.Send(cqrsInput, context.CancellationToken);
            return (output ?? throw new InvalidOperationException()).Adapt<TResponse>();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<TResponse> Handle<TCommand, TResponse>(ServerCallContext context)
    {
        try
        {
            var cqrsInput = Activator.CreateInstance<TCommand>();
            if (cqrsInput is null)
            {
                throw new ArgumentNullException(nameof(cqrsInput));
            }

            var output = await _sender.Send(cqrsInput, context.CancellationToken);
            return (output ?? throw new InvalidOperationException()).Adapt<TResponse>();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Empty> Handle<TRequest, TCommand>(TRequest request,
        ServerCallContext context)
    {
        try
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var cqrsInput = request.Adapt<TCommand>();

            if (cqrsInput is null)
            {
                throw new ArgumentNullException(nameof(cqrsInput));
            }

            await _sender.Send(cqrsInput, context.CancellationToken);
            return new Empty();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
