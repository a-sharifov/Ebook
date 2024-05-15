﻿using Domain.UserAggregate.ValueObjects;

namespace Application.Users.Commands.ForgotPassword;

internal sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(Email.MaxLength);

        RuleFor(x => x.ReturnUrl)
            .NotEmpty();
    }
}
