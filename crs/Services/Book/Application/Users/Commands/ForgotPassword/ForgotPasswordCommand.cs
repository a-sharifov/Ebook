namespace Application.Users.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(
    string Email,
    string ReturnUrl
    ) : ICommand;
