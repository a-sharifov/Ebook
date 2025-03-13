using Domain.OrderAggregate.Entities;
using Domain.OrderAggregate.Errors;
using Domain.OrderAggregate.Events;
using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.ValueObjects;

namespace Domain.OrderAggregate;

public class Order : AggregateRoot<OrderId>
{
    public Guid UserId { get; private set; }
    public OrderDate OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }
    public ShippingAddress ShippingAddress { get; private set; }
    public decimal TotalAmount => CalculateTotalAmount();

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Order() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private Order(
        OrderId id,
        Guid userId,
        OrderDate orderDate,
        OrderStatus status,
        ShippingAddress shippingAddress,
        List<OrderItem> items)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        Status = status;
        ShippingAddress = shippingAddress;
        _items = items;
    }

    public static Result<Order> Create(
        OrderId id,
        Guid userId,
        ShippingAddress shippingAddress,
        List<OrderItem> items)
    {
        if (items.Count == 0)
        {
            return Result.Failure<Order>(OrderErrors.OrderMustHaveItems);
        }

        var order = new Order(
            id,
            userId,
            OrderDate.Now(),
            OrderStatus.Pending,
            shippingAddress,
            items);

        order.AddDomainEvent(new OrderCreatedDomainEvent(
            Guid.NewGuid(),
            id,
            userId,
            order.TotalAmount));

        return Result.Success(order);
    }

    public Result UpdateStatus(OrderStatus newStatus)
    {
        // Validate status transitions
        if (Status == OrderStatus.Cancelled)
        {
            return Result.Failure(OrderStatusErrors.CannotChangeStatusOfCancelledOrder);
        }

        if (Status == OrderStatus.Delivered && newStatus != OrderStatus.Returned)
        {
            return Result.Failure(OrderStatusErrors.DeliveredOrdersCanOnlyBeReturned);
        }

        if ((Status == OrderStatus.Shipped || Status == OrderStatus.Delivered) && newStatus == OrderStatus.Cancelled)
        {
            return Result.Failure(OrderErrors.OrderCannotBeCancelled);
        }

        var oldStatus = Status;
        Status = newStatus;

        AddDomainEvent(new OrderStatusChangedDomainEvent(
            Guid.NewGuid(),
            Id,
            oldStatus,
            newStatus));

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
        {
            return Result.Failure(OrderErrors.OrderCannotBeCancelled);
        }

        var oldStatus = Status;
        Status = OrderStatus.Cancelled;

        AddDomainEvent(new OrderStatusChangedDomainEvent(
            Guid.NewGuid(),
            Id,
            oldStatus,
            OrderStatus.Cancelled));

        return Result.Success();
    }

    public Result UpdateShippingAddress(ShippingAddress shippingAddress)
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.Processing)
        {
            return Result.Failure(OrderErrors.OrderCannotBeModified);
        }

        ShippingAddress = shippingAddress;
        return Result.Success();
    }

    public Result AddItem(OrderItem item)
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(OrderErrors.OrderCannotBeModified);
        }

        _items.Add(item);
        return Result.Success();
    }

    public Result RemoveItem(OrderItemId itemId)
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(OrderErrors.OrderCannotBeModified);
        }

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            return Result.Failure(OrderErrors.OrderItemNotFound);
        }

        _items.Remove(item);
        return Result.Success();
    }

    public Result UpdateItemQuantity(OrderItemId itemId, OrderItemQuantity quantity)
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(OrderErrors.OrderCannotBeModified);
        }

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            return Result.Failure(OrderErrors.OrderItemNotFound);
        }

        return item.UpdateQuantity(quantity);
    }

    private decimal CalculateTotalAmount()
    {
        return _items.Sum(item => item.TotalPrice);
    }
}
