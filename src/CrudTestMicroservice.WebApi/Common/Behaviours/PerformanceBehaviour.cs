using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using CrudTestMicroservice.Application.Common.Interfaces;

namespace CrudTestMicroservice.WebApi.Common.Behaviours;

public class PerformanceBehaviour : Interceptor
{
    private readonly Stopwatch _timer;
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    public PerformanceBehaviour(ILogger<PerformanceBehaviour> logger, ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _timer.Start();

        var response = await continuation(request, context);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;

            _logger.LogWarning("gRPC Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                requestName, elapsedMilliseconds, userId, request);
        }
        return response;
    }
}

