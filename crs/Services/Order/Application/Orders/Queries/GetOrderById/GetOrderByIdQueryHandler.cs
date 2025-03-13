using Application.Orders.Common.DTOs;
using Domain.OrderAggregate.Errors;
using Domain.OrderAggregate.Ids;
using Domain.OrderAggregate.Repositories;

namespace Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderId = new OrderId(request.OrderId);
        
        if (!await _orderRepository.IsExistAsync(orderId, cancellationToken))
        {
            return Result.Failure<OrderDto>(OrderErrors.OrderNotFound);
        }
        
        var order = await _orderRepository.GetOrderWithItemsAsync(orderId, cancellationToken);
        var orderDto = order.Adapt<OrderDto>();
        return orderDto;
    }
}
