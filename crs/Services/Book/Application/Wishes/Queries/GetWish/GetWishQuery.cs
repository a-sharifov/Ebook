using Application.Common.DTOs.Wishes;

namespace Application.Wishes.Queries.GetWish;

public sealed record GetWishQuery(
    Guid UserId) : IQuery<WishDto>;
