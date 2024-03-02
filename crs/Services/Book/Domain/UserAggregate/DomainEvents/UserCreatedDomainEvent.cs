using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.DomainEvents;

public sealed record UserCreatedDomainEvent(Guid Id, UserId UserId) : DomainEvent(Id);
