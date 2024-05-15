namespace Application.Users.Commands.ConfirmForgotPassword;

internal sealed class ConfirmPasswordConfirmCommandValidator : AbstractValidator<ConfirmForgotPasswordCommand>
{
    public ConfirmPasswordConfirmCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.ResetPasswordToken)
            .NotEmpty();
    }
}
