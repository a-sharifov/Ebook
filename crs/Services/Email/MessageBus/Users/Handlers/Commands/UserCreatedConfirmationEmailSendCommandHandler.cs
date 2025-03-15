using Application.Emails.Commands;
using Application.Emails.Commands.SendConfirmationUserMessage;
using Contracts.Services.Users.Commands;
using EventBus.MassTransit.Handlers;
using MassTransit;

namespace MessageBus.Users.Handlers.Commands;

public sealed class UserCreatedConfirmationEmailSendCommandHandler(ISender sender)
        : IntegrationCommandHandler<UserCreatedConfirmationEmailSendCommand>
{
    private readonly ISender _sender = sender;

    public override async Task Handle(ConsumeContext<UserCreatedConfirmationEmailSendCommand> context)
    {
        var message = context.Message;

        await _sender.Send(new SendConfirmationUserMessageCommand(message.UserId, message.ReturnUrl));
    }
}
