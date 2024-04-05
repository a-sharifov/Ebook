using Application.Common.DTOs.Users;

namespace Application.Users.Queries.GetUserByIdString;

public sealed record GetUserByIdStringQuery(string Id) : IQuery<UserDto>;
