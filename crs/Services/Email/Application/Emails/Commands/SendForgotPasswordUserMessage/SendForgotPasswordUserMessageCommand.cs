namespace Application.Emails.Commands.SendConfirmationUserMessage;

public sealed record SendForgotPasswordUserMessageCommand(
    Guid UserId,
    string ReturnUrl) : ICommand;
