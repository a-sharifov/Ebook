using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate.ValueObjects;

namespace Application.Books.Commands.UpdateBook;

internal sealed class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .MaximumLength(Title.MaxLength)
            .NotEmpty();

        RuleFor(x => x.PageCount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.LanguageId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.AuthorId)
            .NotEmpty();

        RuleFor(x => x.GenreId)
            .NotEmpty();
    }
}