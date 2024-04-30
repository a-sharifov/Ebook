using Domain.UserAggregate.Ids;
using Domain.UserAggregate.ValueObjects;
using System.Linq.Expressions;

namespace Domain.UserAggregate.Repositories;

public interface IUserRepository : IBaseRepository<User, UserId>
{
    Task<User> GetAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> IsEmailConfirmedAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<bool> IsExistAsync(Email email, CancellationToken cancellationToken = default);
}
