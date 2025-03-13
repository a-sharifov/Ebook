namespace Presentation.V1.Models;

public record CreateOrderRequest(
    Guid UserId,
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode,
    List<CreateOrderItemRequest> Items
);

public record CreateOrderItemRequest(
    Guid BookId,
    string BookTitle,
    decimal UnitPrice,
    int Quantity
);
