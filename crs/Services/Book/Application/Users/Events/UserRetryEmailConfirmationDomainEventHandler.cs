using Contracts.Services.Users.Commands;
using Domain.UserAggregate.DomainEvents;
using EventBus.MassTransit.Abstractions;

namespace Application.Users.Events;

internal sealed class UserRetryEmailConfirmationDomainEventHandler(IMessageBus bus)
        : IDomainEventHandler<UserRetryEmailConfirmationDomainEvent>
{
    private readonly IMessageBus _bus = bus;

    public async Task Handle(UserRetryEmailConfirmationDomainEvent notification, CancellationToken cancellationToken)
    {
        var endpoint =
            await _bus.GetSendEndpoint<UserRetryEmailConfirmationSendCommand>();

        var @event =
            new UserRetryEmailConfirmationSendCommand(
                Guid.NewGuid(),
                notification.UserId.Value,
                notification.ReturnUrl);

        await endpoint.Send(@event, cancellationToken);
    }
}

