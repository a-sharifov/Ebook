using Application.Common.DTOs.Images;

namespace Application.Common.DTOs.Books;

public sealed record BookDto(
    Guid Id,
    string Title,
    int PageCount,
    int Price,
    string ISBN,
    int Quantity,
    int SoldUnits,
    ImageDto Poster
    );
