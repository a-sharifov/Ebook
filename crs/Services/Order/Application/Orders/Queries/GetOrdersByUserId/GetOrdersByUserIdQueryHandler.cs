using Application.Orders.Common.DTOs;
using Domain.OrderAggregate.Repositories;

namespace Application.Orders.Queries.GetOrdersByUserId;

public class GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrdersByUserIdQuery, List<OrderDto>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Result<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(request.UserId, cancellationToken);

        if (!orders.Any())
        {
            return Result.Success(new List<OrderDto>());
        }

        var ordersDto = orders.Adapt<List<OrderDto>>();
        return ordersDto;
    }
}
