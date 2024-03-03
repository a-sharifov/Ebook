using Domain.UserAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;

namespace Persistence.Repositories;

public class UserRepository : IUserRepository
{ 

    public Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task ChangeEmailAsync(UserId userId, Email newEmail, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task ChangePasswordAsync(UserId userId, string currentPassowrd, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckPasswordAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task ConfirmEmailAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsEmailConfirmedAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsEmailUnigueAsync(Email email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserExistsAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserExistsAsync(Email userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}
