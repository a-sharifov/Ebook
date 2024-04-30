namespace Application.Carts.Queries.GetQuantityBookCart;

public sealed record GetQuantityCartBookQuery(Guid UserId) : IQuery<int>;
