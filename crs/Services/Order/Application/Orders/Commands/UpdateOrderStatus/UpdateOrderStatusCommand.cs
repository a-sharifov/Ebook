using Domain.OrderAggregate.ValueObjects;
using Application.Core.CQRS.Command;

namespace Application.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(
    Guid OrderId,
    OrderStatus NewStatus) : ICommand<Order>;
