using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate.Events;

public sealed record OrderStatusChangedDomainEvent(
    Guid Id,
    OrderId OrderId,
    OrderStatus OldStatus,
    OrderStatus NewStatus) : DomainEvent(Id);
