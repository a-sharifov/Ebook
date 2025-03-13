using Application.Orders.Common.DTOs;
using Domain.OrderAggregate.Entities;

namespace Application.Orders.Common.Mappings;

public static class OrderMappings
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            order.Id.Value,
            order.UserId,
            order.OrderDate.Value,
            order.Status,
            order.ShippingAddress.ToString(),
            order.TotalAmount,
            order.Items.Select(item => item.ToDto()).ToList());
    }

    public static OrderItemDto ToDto(this OrderItem item)
    {
        return new OrderItemDto(
            item.Id.Value,
            item.BookId,
            item.BookTitle,
            item.UnitPrice,
            item.Quantity.Value,
            item.TotalPrice);
    }
}
