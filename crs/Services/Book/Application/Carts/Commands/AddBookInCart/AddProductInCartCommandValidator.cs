namespace Application.Carts.Commands.AddBookInCart;

internal sealed class AddProductInCartCommandValidator : AbstractValidator<AddProductInCartCommand>
{
    public AddProductInCartCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}