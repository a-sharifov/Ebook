using Application.Orders.Common.DTOs;

namespace Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<OrderDto>;
