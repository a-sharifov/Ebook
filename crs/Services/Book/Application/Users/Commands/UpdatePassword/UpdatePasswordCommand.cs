namespace Application.Users.Commands.UpdatePassword;

public sealed record UpdatePasswordCommand(
    Guid UserId,
    string OldPassword,
    string NewPassword
    ) : ICommand;
