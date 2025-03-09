﻿using Application.Emails.Commands;
using Contracts.Services.Users.Commands;
using EventBus.MassTransit.Handlers;
using MassTransit;

namespace MessageBus.Users.Handlers.Commands;

public sealed class UserRetryEmailConfirmationSendCommandHandler(ISender sender)
        : IntegrationCommandHandler<UserRetryEmailConfirmationSendCommand>
{
    private readonly ISender _sender = sender;

    public override async Task Handle(ConsumeContext<UserRetryEmailConfirmationSendCommand> context)
    {
        var message = context.Message;

        await _sender.Send(new SendConfirmationUserMessageCommand(message.UserId, message.ReturnUrl));
    }
}
