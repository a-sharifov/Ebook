using Application.Common.DTOs.Books;

namespace Application.Common.DTOs.Carts.CartIrems;

public sealed record CartItemDto(
    Guid Id,
    BookDto Book,
    int Quantity
    );