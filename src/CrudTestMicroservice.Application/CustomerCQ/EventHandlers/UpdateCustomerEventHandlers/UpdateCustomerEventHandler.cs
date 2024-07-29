using CrudTestMicroservice.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CrudTestMicroservice.Application.CustomerCQ.EventHandlers;

public class UpdateCustomerEventHandler : INotificationHandler<UpdateCustomerEvent>
{
    private readonly ILogger<
        UpdateCustomerEventHandler> _logger;

    public UpdateCustomerEventHandler(ILogger<UpdateCustomerEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UpdateCustomerEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
