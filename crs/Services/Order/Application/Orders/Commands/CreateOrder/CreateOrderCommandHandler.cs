using Domain.Core.UnitOfWorks.Interfaces;
using Domain.OrderAggregate.Entities;
using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.Repositories;
using Domain.OrderAggregate.ValueObjects;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
            var shippingAddressResult = ShippingAddress.Create(
                request.Street,
                request.City,
                request.State,
                request.Country,
                request.ZipCode);

            if (shippingAddressResult.IsFailure)
            {
                return Result.Failure(shippingAddressResult.Error);
            }

            var orderItems = new List<OrderItem>();
            var orderId = new OrderId(Guid.NewGuid());

            foreach (var itemRequest in request.Items)
            {
                var quantityResult = OrderItemQuantity.Create(itemRequest.Quantity);
                if (quantityResult.IsFailure)
                {
                    return Result.Failure<Guid>(quantityResult.Error);
                }

                var orderItemResult = OrderItem.Create(
                    new OrderItemId(Guid.NewGuid()),
                    orderId,
                    itemRequest.BookId,
                    itemRequest.BookTitle,
                    itemRequest.UnitPrice,
                    quantityResult.Value);

                if (orderItemResult.IsFailure)
                {
                    return Result.Failure(orderItemResult.Error);
                }

                orderItems.Add(orderItemResult.Value);
            }

            var orderResult = Order.Create(
                orderId,
                request.UserId,
                shippingAddressResult.Value,
                orderItems);

            if (orderResult.IsFailure)
            {
                return Result.Failure<Guid>(orderResult.Error);
            }

            var order = orderResult.Value;

            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
    }
}
