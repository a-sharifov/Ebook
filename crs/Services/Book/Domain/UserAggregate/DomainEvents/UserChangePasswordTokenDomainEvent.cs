using Domain.Core.Events.Interfaces;
using Domain.UserAggregate.Ids;

namespace Domain.UserAggregate.DomainEvents;

public sealed record UserChangePasswordTokenDomainEvent(
    Guid Id,
    UserId UserId,
    string ReturnUrl)
    : IDomainEvent;
