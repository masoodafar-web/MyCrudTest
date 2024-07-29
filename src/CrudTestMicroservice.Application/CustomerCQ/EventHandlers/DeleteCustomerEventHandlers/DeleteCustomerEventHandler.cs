using CrudTestMicroservice.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CrudTestMicroservice.Application.CustomerCQ.EventHandlers;

public class DeleteCustomerEventHandler : INotificationHandler<DeleteCustomerEvent>
{
    private readonly ILogger<
        DeleteCustomerEventHandler> _logger;

    public DeleteCustomerEventHandler(ILogger<DeleteCustomerEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DeleteCustomerEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
