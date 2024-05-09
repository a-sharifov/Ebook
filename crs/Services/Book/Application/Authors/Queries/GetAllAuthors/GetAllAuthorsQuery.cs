using Application.Common.DTOs.Authors;

namespace Application.Authors.Queries.GetAllAuthors;

public sealed record GetAllAuthorsQuery : IQuery<IEnumerable<AuthorDto>>;
