using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate.Entities;

public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; }
    public Guid BookId { get; private set; }
    public string BookTitle { get; private set; }
    public decimal UnitPrice { get; private set; }
    public OrderItemQuantity Quantity { get; private set; }
    public decimal TotalPrice => UnitPrice * Quantity;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private OrderItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private OrderItem(
        OrderItemId id,
        OrderId orderId,
        Guid bookId,
        string bookTitle,
        decimal unitPrice,
        OrderItemQuantity quantity)
    {
        Id = id;
        OrderId = orderId;
        BookId = bookId;
        BookTitle = bookTitle;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }


    public static Result<OrderItem> Create(
        OrderItemId id,
        OrderId orderId,
        Guid bookId,
        string bookTitle,
        decimal unitPrice,
        OrderItemQuantity quantity)
    {
        var orderItem = new OrderItem(id, orderId, bookId, bookTitle, unitPrice, quantity);
        return Result.Success(orderItem);
    }

    public Result UpdateQuantity(OrderItemQuantity quantity)
    {
        Quantity = quantity;
        return Result.Success();
    }
}
