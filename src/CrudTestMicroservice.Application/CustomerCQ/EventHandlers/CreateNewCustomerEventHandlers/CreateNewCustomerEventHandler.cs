using CrudTestMicroservice.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CrudTestMicroservice.Application.CustomerCQ.EventHandlers;

public class CreateNewCustomerEventHandler : INotificationHandler<CreateNewCustomerEvent>
{
    private readonly ILogger<CreateNewCustomerEventHandler> _logger;

    public CreateNewCustomerEventHandler(ILogger<CreateNewCustomerEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CreateNewCustomerEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
