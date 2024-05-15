using Application.Users.Commands.Login;

namespace Application.Users.Commands.LoginByForgotPassword;

public sealed record LoginByForgotPasswordCommand(
    Guid UserId,
    string ResetPasswordToken
    ) : ICommand<LoginCommandResponse>;
    
