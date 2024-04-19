using Catalog.Persistence.Caching.Abstractions;
using Domain.UserAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrasctrurcture.Core.Extensions;
using Persistence.DbContexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class UserRepository(
    BookDbContext dbContext,
    ICachedEntityService<User, UserId> cached)
    : BaseRepository<User, UserId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), IUserRepository
{
    public async Task<User> GetByEmailAsync(Email email, Expression<Func<User, object>>[]? includes = default, CancellationToken cancellationToken = default)
    {
        var user = await GetEntityDbSet()
            .Includes(includes)
            .FirstAsync(x => x.Email == email, cancellationToken: cancellationToken);

        await _cached.SetAsync(user, cancellationToken: cancellationToken);

        return user;
    }

    public async Task<bool> IsEmailConfirmedAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var user = await GetEntityDbSet().FirstAsync(
            x => x.Id == userId, cancellationToken: cancellationToken);

        return user.IsEmailConfirmed;
    }

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) =>
        !await IsEmailExistAsync(email, cancellationToken);

    public async Task<bool> IsEmailExistAsync(Email email, CancellationToken cancellationToken = default) =>
     await GetEntityDbSet()
     .AnyAsync(u => u.Email == email, cancellationToken);
}
