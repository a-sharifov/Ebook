using Application.Common.DTOs.Genres;

namespace Application.Genres.Queries.GetGenreById;

public sealed record GetGenreByIdQuery(Guid Id) : IQuery<GenreDto>;
