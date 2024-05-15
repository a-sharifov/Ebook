namespace Application.Users.Commands.LoginByForgotPassword;

internal sealed class LoginByForgotPasswordCommandValidator : AbstractValidator<LoginByForgotPasswordCommand>
{
    public LoginByForgotPasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.ForgotPasswordToken)
            .NotEmpty();
    }
}
    
