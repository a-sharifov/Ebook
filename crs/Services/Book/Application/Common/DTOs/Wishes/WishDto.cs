using Application.Common.DTOs.Wishes.WishItems;

namespace Application.Common.DTOs.Wishes;

public sealed record WishDto(
    Guid Id,
    Guid UserId,
    IEnumerable<WishItemDto> Items
    );
