using Contracts.Services.Users.Commands;
using Domain.UserAggregate.DomainEvents;
using EventBus.MassTransit.Abstractions;
 
namespace Application.Users.Events;

internal sealed class UserConfirmCreatedDomainEventHandler(IMessageBus bus)
        : IDomainEventHandler<UserConfirmCreatedDomainEvent>
{
    private readonly IMessageBus _bus = bus;

    public async Task Handle(UserConfirmCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var endpoint = 
            await _bus.GetSendEndpoint<UserCreatedConfirmationEmailSendCommand>();

        var @event = 
            new UserCreatedConfirmationEmailSendCommand(
                Guid.NewGuid(),
                notification.Id,
                notification.ReturnUrl);

        await endpoint.Send(@event, cancellationToken);
    }
}
