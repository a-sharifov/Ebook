namespace Application.Orders.Commands.CancelOrder;

public record CancelOrderCommand(Guid OrderId) : ICommand;
