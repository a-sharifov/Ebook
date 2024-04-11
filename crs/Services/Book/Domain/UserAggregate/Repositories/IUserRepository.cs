using Domain.UserAggregate.Ids;
using Domain.UserAggregate.ValueObjects;

namespace Domain.UserAggregate.Repositories;

public interface IUserRepository : IBaseRepository<User, UserId>
{
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> IsEmailConfirmedAsync(UserId userId, CancellationToken cancellationToken = default);
}
