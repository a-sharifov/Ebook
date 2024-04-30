namespace Application.Carts.Commands.UpdateCountBookInCart;

public sealed record UpdateCountBookInCart(
    Guid CartItemId,
    int Quantity
    ) : ICommand;
