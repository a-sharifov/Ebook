using Application.Orders.Common.DTOs;

namespace Application.Orders.Queries.GetOrdersByUserId;

public record GetOrdersByUserIdQuery(Guid UserId) : IQuery<List<OrderDto>>;
