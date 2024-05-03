namespace Application.Wishes.Queries.BookInWIsh;

public sealed record BookInWishQuery(Guid UserId, Guid BookId) : IQuery<bool>;
