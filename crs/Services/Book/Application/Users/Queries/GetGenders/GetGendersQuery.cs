namespace Application.Users.Queries.GetGenders;

public sealed record GetGendersQuery() : IQuery<IEnumerable<string>>;
