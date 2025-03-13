namespace Application.Orders.Common.DTOs;

public record OrderItemDto(
    Guid Id,
    Guid BookId,
    string BookTitle,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice);
