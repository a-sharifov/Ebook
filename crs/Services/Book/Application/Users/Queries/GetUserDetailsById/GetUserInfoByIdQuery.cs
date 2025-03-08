using Application.Common.DTOs.Users;

namespace Application.Users.Queries.GetUserById;

public sealed record GetUserDetailsByIdQuery(Guid Id) : IQuery<UserDetailsDto>;

