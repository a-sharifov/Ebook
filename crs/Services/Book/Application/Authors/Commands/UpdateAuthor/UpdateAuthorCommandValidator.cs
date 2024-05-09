using Domain.AuthorAggregate.ValueObjects;

namespace Application.Authors.Commands.UpdateAuthor;

internal sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Pseudonym)
            .MaximumLength(Pseudonym.MaxLength)
            .NotEmpty();
    }
}