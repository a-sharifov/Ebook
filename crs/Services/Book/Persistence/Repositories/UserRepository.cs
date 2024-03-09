using Catalog.Persistence.Caching.Abstractions;
using Domain.UserAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class UserRepository(
    BookDbContext dbContext,
    ICachedEntityService<User, UserId> cached)
    : BaseRepository<User, UserId>(
        dbContext,
        cached,
        expirationTime: TimeSpan.FromMinutes(20)), IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext
        .Set<User>()
        .FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);

        if (user is null)
        {
            return user;
        }

        await _cached.SetAsync(user, cancellationToken: cancellationToken);

        return user;
    }

    public async Task<bool> IsEmailConfirmedAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext
            .Set<User>()
            .FirstAsync(x => x.Id == userId, cancellationToken: cancellationToken);

        return user.IsEmailConfirmed;
    }

    public async Task<bool> IsEmailUnigueAsync(Email email, CancellationToken cancellationToken = default) =>
        !await _dbContext
        .Set<User>()
        .AnyAsync(u => u.Email == email, cancellationToken);
}
