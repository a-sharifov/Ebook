using Contracts.Services.Users.Commands;
using Domain.UserAggregate.DomainEvents;
using EventBus.MassTransit.Abstractions;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Events;

internal class UserChangePasswordTokenDomainEventHandler(
 IMessageBus bus)
: IDomainEventHandler<UserChangePasswordTokenDomainEvent>
{
    private readonly IMessageBus _bus = bus;

    public async Task Handle(UserChangePasswordTokenDomainEvent notification, CancellationToken cancellationToken)
    {
        var endpoint =
          await _bus.GetSendEndpoint<UserChangePasswordEmailSendCommand>();

        var @event =
            new UserChangePasswordEmailSendCommand(
                Guid.NewGuid(),
                notification.UserId.Value,
                notification.ReturnUrl);

        await endpoint.Send(@event, cancellationToken);
    }
}
