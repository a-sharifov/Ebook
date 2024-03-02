using Application.Users.Common;

namespace Application.Users.Queries.GetUserInfoByIdString;

public sealed record GetUserInfoByIdStringQuery(string Id) : IQuery<UserDto>;
