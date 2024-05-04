namespace Application.Carts.Commands.UpdateQuantityBookInCart;

internal sealed class UpdateQuantityBookInCartValidator : AbstractValidator<UpdateQuantityBookInCart>
{
    public UpdateQuantityBookInCartValidator()
    {
        RuleFor(x => x.CartItemId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);
    }
} 