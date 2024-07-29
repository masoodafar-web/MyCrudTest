using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using CrudTestMicroservice.Application.Common.Interfaces;

namespace CrudTestMicroservice.WebApi.Common.Behaviours;

public class LoggingBehaviour : Interceptor
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    public LoggingBehaviour(ILogger<LoggingBehaviour> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        _logger.LogInformation("gRPC Starting receiving call. Type/Method: {Type} / {Method} Request: {Name} {@UserId} {@Request}",
            MethodType.Unary, context.Method , requestName, userId, request);

        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "gRPC Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
