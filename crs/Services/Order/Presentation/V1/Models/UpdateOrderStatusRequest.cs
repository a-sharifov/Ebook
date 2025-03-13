using Domain.OrderAggregate.ValueObjects;

namespace Presentation.V1.Models;

public record UpdateOrderStatusRequest(OrderStatus Status);
