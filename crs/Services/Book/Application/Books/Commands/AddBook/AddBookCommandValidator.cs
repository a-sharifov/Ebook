namespace Application.Books.Commands.AddBook;

internal sealed class AddBookCommandValidator : AbstractValidator<AddBookCommand>
{
    public AddBookCommandValidator()
    {
        RuleFor(x => x.Price);
    }
}