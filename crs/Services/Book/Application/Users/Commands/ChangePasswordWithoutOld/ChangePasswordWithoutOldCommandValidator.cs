namespace Application.Users.Commands.ChangePasswordWithoutOld;

internal sealed class ChangePasswordWithoutOldCommandValidator : AbstractValidator<ChangePasswordWithoutOldCommand>
{
    public ChangePasswordWithoutOldCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.ReturnUrl)
            .NotEmpty();
    }
}

