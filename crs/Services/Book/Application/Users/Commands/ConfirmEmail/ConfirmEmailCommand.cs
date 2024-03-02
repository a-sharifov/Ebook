namespace Application.Users.Commands.ConfirmEmail;

public sealed record ConfirmEmailCommand(
    Guid UserId, 
    string EmailConfirmationToken) : ICommand;
