using Domain.OrderAggregate.ValueObjects;

namespace Application.Orders.Common.DTOs;

public record OrderDto(
    Guid Id,
    Guid UserId,
    DateTime OrderDate,
    OrderStatus Status,
    string ShippingAddress,
    decimal TotalAmount,
    IReadOnlyList<OrderItemDto> Items);
