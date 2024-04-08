using Domain.UserAggregate.ValueObjects;

namespace Application.Users.Commands.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(Email.MaxLength);

        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.Audience)
            .NotEmpty();
    }
}
