namespace Application.Carts.Commands.UpdateQuantityBookInCart;

public sealed record UpdateQuantityBookInCart(
    Guid CartItemId,
    int Quantity
    ) : ICommand;
