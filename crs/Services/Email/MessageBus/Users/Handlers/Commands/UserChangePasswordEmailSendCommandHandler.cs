using Application.Emails.Commands.SendChangePasswordMessage;
using Contracts.Services.Users.Commands;
using EventBus.MassTransit.Handlers;
using MassTransit;

namespace MessageBus.Users.Handlers.Commands;

public sealed class UserChangePasswordEmailSendCommandHandler(ISender sender)
        : IntegrationCommandHandler<UserChangePasswordEmailSendCommand>
{
    private readonly ISender _sender = sender;

    public override async Task Handle(ConsumeContext<UserChangePasswordEmailSendCommand> context)
    {
        var message = context.Message;

        await _sender.Send(new SendChangePasswordMessageCommand(message.UserId, message.ReturnUrl));
    }
}
