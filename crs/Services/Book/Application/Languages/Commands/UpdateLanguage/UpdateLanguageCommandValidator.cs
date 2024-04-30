using Domain.LanguageAggregate.ValueObjects;

namespace Application.Languages.Commands.UpdateLanguage;

internal sealed class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
{
    public UpdateLanguageCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .MaximumLength(LanguageName.MaxLength)
            .NotEmpty();

        RuleFor(x => x.Code)
            .MaximumLength(LanguageCode.MaxLength)
            .NotEmpty();
    }
}