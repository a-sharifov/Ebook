using Domain.AuthorAggregate.ValueObjects;
using Domain.BookAggregate.ValueObjects;

namespace Application.Books.Commands.AddBook;

internal sealed class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
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

        RuleFor(x => x.Poster)
            .NotEmpty();

        RuleFor(x => x.PosterStream)
            .NotEmpty();
    }
}