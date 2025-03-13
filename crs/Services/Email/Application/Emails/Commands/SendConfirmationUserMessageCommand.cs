namespace Application.Emails.Commands;

public sealed record SendConfirmationUserMessageCommand(
    Guid UserId,
    string ReturnUrl) : ICommand;
