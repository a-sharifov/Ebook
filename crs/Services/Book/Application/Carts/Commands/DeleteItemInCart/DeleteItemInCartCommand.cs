
namespace Application.Carts.Commands.DeleteItemInCart;

public sealed record DeleteItemInCartCommand(Guid ItemId) : ICommand;
