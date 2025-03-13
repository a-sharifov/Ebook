using Common.Contracts.Services.Orders.Events;
using EventBus.MassTransit.Handlers;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MessageBus.Orders.Handlers.Events;

public class OrderStatusChangedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<OrderStatusChangedIntegrationEvent>
{

    private readonly ISender _sender = sender;

    public async override Task Handle(ConsumeContext<OrderStatusChangedIntegrationEvent> context)
    {

    }
}
