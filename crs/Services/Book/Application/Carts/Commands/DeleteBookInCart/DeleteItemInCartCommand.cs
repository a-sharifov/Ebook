
namespace Application.Carts.Commands.DeleteBookInCart;

public sealed record DeleteItemInCartCommand(Guid ItemId) : ICommand;
