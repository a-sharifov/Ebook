using Domain.OrderAggregate.ValueObjects;

namespace Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid UserId,
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode,
    List<CreateOrderItemCommand> Items) : ICommand;

public record CreateOrderItemCommand(
    Guid BookId,
    string BookTitle,
    decimal UnitPrice,
    int Quantity);
