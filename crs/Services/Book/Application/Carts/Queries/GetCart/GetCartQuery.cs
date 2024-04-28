using Application.Common.DTOs.Carts;

namespace Application.Carts.Queries.GetCart;

public sealed record GetCartQuery(Guid UserId) : IQuery<CartDto>;
