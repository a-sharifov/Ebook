namespace Application.Carts.Commands.AddProductInCart;

public sealed record AddProductInCartCommand(
    Guid UserId,
    Guid BookId,
    int Quantity
    ) : ICommand;
