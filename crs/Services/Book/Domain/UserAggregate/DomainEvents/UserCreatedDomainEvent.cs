using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.DomainEvents;

public sealed record UserConfirmCreatedDomainEvent(
    Guid Id, 
    UserId UserId, 
    string ReturnUrl) : DomainEvent(Id);
