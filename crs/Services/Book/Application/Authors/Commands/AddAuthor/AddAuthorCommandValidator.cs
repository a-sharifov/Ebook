using Domain.AuthorAggregate.ValueObjects;

namespace Application.Authors.Commands.AddAuthor;

internal sealed class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator()
    {
        RuleFor(x => x.Pseudonym)
            .NotEmpty()
            .MaximumLength(Pseudonym.MaxLength);
    }
}
