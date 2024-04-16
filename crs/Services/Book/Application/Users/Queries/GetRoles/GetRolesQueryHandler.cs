using Contracts.Enumerations;

namespace Application.Users.Queries.GetRoles;

internal sealed class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, IEnumerable<string>>
{
    public Task<Result<IEnumerable<string>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = Role.GetNames();
        var response = Result.Success(roles);
        return Task.FromResult(response);
    }
}
