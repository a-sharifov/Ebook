using Domain.Core.Events.Interfaces;
using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.DomainEvents;

public sealed record UserRetryEmailConfirmationDomainEvent(
    Guid Id, 
    UserId UserId) 
    : IDomainEvent;
