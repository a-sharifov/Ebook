namespace Application.Carts.Queries.BookInCart;

public sealed record BookInCartQuery(Guid UserId, Guid BookId) : IQuery<bool>;
