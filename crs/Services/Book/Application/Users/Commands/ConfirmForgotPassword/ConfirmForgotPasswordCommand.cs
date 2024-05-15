namespace Application.Users.Commands.ConfirmForgotPassword;

public sealed record ConfirmForgotPasswordCommand(
    Guid Id,
    string ResetPasswordToken) : ICommand;
