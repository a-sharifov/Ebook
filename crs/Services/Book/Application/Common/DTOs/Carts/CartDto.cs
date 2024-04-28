using Application.Common.DTOs.Carts.CartIrems;

namespace Application.Common.DTOs.Carts;

public sealed record CartDto(
    Guid Id,
    IEnumerable<CartItemDto> Items,
    decimal TotalPrice 
    );