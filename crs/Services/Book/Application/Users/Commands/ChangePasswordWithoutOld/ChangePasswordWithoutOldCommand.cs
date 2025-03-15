namespace Application.Users.Commands.ChangePasswordWithoutOld;

public sealed record ChangePasswordWithoutOldCommand(
    Guid UserId,
    string ReturnUrl) : ICommand;

