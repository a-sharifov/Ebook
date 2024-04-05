using Application.Common.DTOs.Images;

namespace Application.Common.DTOs.Genres;

public sealed record class GenreDto(
    Guid Id,
    string Name,
    ImageDto Image
    );
