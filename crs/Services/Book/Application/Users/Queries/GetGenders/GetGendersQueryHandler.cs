using Contracts.Enumerations;

namespace Application.Users.Queries.GetGenders;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetGendersQuery, IEnumerable<string>>
{
    public Task<Result<IEnumerable<string>>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
    {
        var roles = Gender.GetNames();
        var response = Result.Success(roles);
        return Task.FromResult(response);
    }
}
