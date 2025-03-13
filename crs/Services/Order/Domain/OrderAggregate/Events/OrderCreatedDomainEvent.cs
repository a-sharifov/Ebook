using Domain.OrderAggregate.Ids;

namespace Domain.OrderAggregate.Events;

public sealed record OrderCreatedDomainEvent(
    Guid Id,
    OrderId OrderId,
    Guid UserId,
    decimal TotalAmount) : DomainEvent(Id);
