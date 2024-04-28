namespace Application.Carts.Commands.ClearCart;

public sealed record ClearCartCommand(Guid Id) : ICommand;
