using Application.Common.DTOs.Authors;

namespace Application.Authors.Queries.GetAuthorById;

public sealed record GetAuthorByIdQuery(Guid Id) : IQuery<AuthorDto>;
