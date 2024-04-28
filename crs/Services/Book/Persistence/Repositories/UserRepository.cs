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
    public async Task<User> GetAsync(Email email, CancellationToken cancellationToken = default)
    {
        var user = await GetEntityDbSet()
            .FirstAsync(x => x.Email == email, cancellationToken: cancellationToken);

        await _cached.SetAsync(user, cancellationToken: cancellationToken);

        return user;
    }

    public async Task<bool> IsEmailConfirmedAsync(UserId id, CancellationToken cancellationToken = default)
    {
        var user = await _cached.GetAsync(id, cancellationToken);

        if(user is not null)
        {
            return user.IsEmailConfirmed;
        }

        user = await GetEntityDbSet().FirstAsync(
            x => x.Id == id, cancellationToken: cancellationToken);

        return user.IsEmailConfirmed;
    }

    public async Task<bool> IsExistAsync(Email email, CancellationToken cancellationToken = default) =>
        await GetEntityDbSet().AnyAsync(
            u => u.Email == email, cancellationToken);

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default) =>
        !await IsExistAsync(email, cancellationToken);

}
