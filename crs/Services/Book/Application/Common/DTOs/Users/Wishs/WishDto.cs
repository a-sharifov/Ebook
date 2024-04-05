using Application.Common.DTOs.Books;

namespace Application.Common.DTOs.Users.Wishs;

public sealed record WishDto(
    Guid Id,
    BookDto Book
    );
