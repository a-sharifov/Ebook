using Domain.Core.Events.Interfaces;
using Domain.UserAggregate.DomainEvents;

namespace Application.Users.Events;

internal sealed class UserCreatedDomainEventHandler
    : IDomainEventHandler<UserCreatedDomainEvent>
{

    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
