namespace Application.Users.Commands.ChangePassword;

public sealed record ChangePasswordCommand(
    Guid UserId,
    string OldPassword,
    string NewPassword
    ) : ICommand;
