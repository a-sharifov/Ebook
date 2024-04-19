using Domain.UserAggregate.Ids;
using Domain.UserAggregate.ValueObjects;
using System.Linq.Expressions;

namespace Domain.UserAggregate.Repositories;

public interface IUserRepository : IBaseRepository<User, UserId>
{
    Task<User> GetByEmailAsync(Email email, Expression<Func<User, object>>[]? includes = default, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> IsEmailConfirmedAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<bool> IsEmailExistAsync(Email email, CancellationToken cancellationToken = default);
}
