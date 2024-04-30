namespace Application.Carts.Commands.AddBookInCart;

public sealed record AddProductInCartCommand(
    Guid UserId,
    Guid BookId
    ) : ICommand;
