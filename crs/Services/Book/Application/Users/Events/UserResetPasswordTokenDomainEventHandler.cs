using Contracts.Services.Users.Commands;
using Domain.UserAggregate.DomainEvents;
using EventBus.MassTransit.Abstractions;
using MassTransit;

namespace Application.Users.Events;

internal sealed class UserResetPasswordTokenDomainEventHandler(
    IMessageBus bus) 
    : IDomainEventHandler<UserResetPasswordTokenDomainEvent>
{
    private readonly IMessageBus _bus = bus;

    public async Task Handle(UserResetPasswordTokenDomainEvent notification, CancellationToken cancellationToken)
    {
        var endpoint =
          await _bus.GetSendEndpoint<UserForgotPasswordEmailConfirmationSendCommand>();

        var @event =
            new UserForgotPasswordEmailConfirmationSendCommand(
                Guid.NewGuid(),
                notification.UserId.Value,
                notification.ReturnUrl);

        await endpoint.Send(@event, cancellationToken);
    }
}
