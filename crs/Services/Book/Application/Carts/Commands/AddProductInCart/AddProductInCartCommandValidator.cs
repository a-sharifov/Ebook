namespace Application.Carts.Commands.AddProductInCart;

internal sealed class AddProductInCartCommandValidator : AbstractValidator<AddProductInCartCommand>
{
    public AddProductInCartCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}