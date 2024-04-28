using Application.Common.DTOs.Authors;
using Application.Common.DTOs.Genres;
using Application.Common.DTOs.Images;
using Application.Common.DTOs.Languages;

namespace Application.Common.DTOs.Books;

public sealed record BookDto(
    Guid Id,
    string Title,
    int PageCount,
    decimal Price,
    LanguageDto Language,
    int Quantity,
    int SoldUnits,
    string PosterUrl,
    AuthorDto Author,
    GenreDto Genre
    );
