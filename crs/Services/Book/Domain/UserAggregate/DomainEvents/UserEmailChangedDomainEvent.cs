using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.DomainEvents;

public sealed record UserEmailChangedDomainEvent(
    Guid Id,
    UserId UserId)
    : DomainEvent(Id);
