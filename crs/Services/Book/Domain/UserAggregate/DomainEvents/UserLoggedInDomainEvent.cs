using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.DomainEvents;

public record UserLoggedInDomainEvent(
    Guid Id,
    UserId UserId)
    : DomainEvent(Id);
