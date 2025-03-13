using EventBus.Common.Messages;

namespace Common.Contracts.Services.Orders.Events;

public sealed record OrderStatusChangedIntegrationEvent(
    Guid Id,
    string OrderId, 
    Guid UserId, 
    string OldStatus, 
    string NewStatus, 
    string UserEmail) : IntegrationEvent(Id);
