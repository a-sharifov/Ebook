using Application.Common.DTOs.Books;

namespace Application.Common.DTOs.Wishes.WishItems;

public record WishItemDto(
    Guid Id,
    BookDto Book
    );
