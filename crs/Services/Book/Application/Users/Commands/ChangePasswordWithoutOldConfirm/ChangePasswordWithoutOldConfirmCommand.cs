namespace Application.Users.Commands.ChangePasswordWithoutOldConfirm;

public sealed record ChangePasswordWithoutOldConfirmCommand(
    Guid Id,
    string Password,
    string ResetPasswordToken) : ICommand;
