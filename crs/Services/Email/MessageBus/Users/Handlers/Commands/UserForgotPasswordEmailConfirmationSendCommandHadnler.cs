using Application.Emails.Commands.SendConfirmationUserMessage;
using Contracts.Services.Users.Commands;
using EventBus.MassTransit.Handlers;
using MassTransit;

namespace MessageBus.Users.Handlers.Commands;

public sealed class UserForgotPasswordEmailConfirmationSendCommandHadnler(ISender sender)
        : IntegrationCommandHandler<UserForgotPasswordEmailConfirmationSendCommand>
{
    private readonly ISender _sender = sender;

    public override async Task Handle(ConsumeContext<UserForgotPasswordEmailConfirmationSendCommand> context)
    {
        var message = context.Message;

        await _sender.Send(new SendForgotPasswordUserMessageCommand(message.UserId, message.ReturnUrl));
    }
}
