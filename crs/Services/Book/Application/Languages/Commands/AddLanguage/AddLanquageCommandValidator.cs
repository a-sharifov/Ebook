using Domain.LanguageAggregate.ValueObjects;

namespace Application.Languages.Commands.AddLanguage;

internal sealed class AddLanquageCommandValidator : AbstractValidator<AddLanquageCommand>
{
    public AddLanquageCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(LanguageName.MaxLength)
            .NotEmpty();   
        
        RuleFor(x => x.Code)
            .MaximumLength(LanguageCode.MaxLength) 
            .NotEmpty();
    }
}
