using Contracts.Enumerations;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.ValueObjects;
using Domain.UserAggregate.Entities;
using Domain.BookAggregate.Ids;
using Domain.CartAggregate;

namespace Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public PasswordSalt PasswordSalt { get; private set; }
    public RefreshToken? RefreshToken { get; private set; }
    public EmailConfirmationToken EmailConfirmationToken { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public Role Role { get; private set; }
    public Gender Gender { get; private set; }
    public Cart Cart { get; private set; }

    private readonly List<Wish> _wishes;
    public IReadOnlyCollection<Wish> Wishes => _wishes.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private User(
        UserId id,
        Email email,
        FirstName firstName,
        LastName lastName,
        PasswordHash passwordHash,
        PasswordSalt passwordSalt,
        EmailConfirmationToken emailConfirmationToken,
        bool isEmailConfirmed,
        Role role,
        Gender gender,
        Cart cart)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        EmailConfirmationToken = emailConfirmationToken;
        IsEmailConfirmed = isEmailConfirmed;
        Role = role;
        Gender = gender;
        Cart = cart;
        _wishes = [];
    }

    public static Result<User> Create(
        UserId id,
        Email email,
        FirstName firstName,
        LastName lastName,
        PasswordHash passwordHash,
        PasswordSalt passwordSalt,
        EmailConfirmationToken emailConfirmationToken,
        bool isEmailUnique,
        Role role,
        Gender gender,
        Cart cart)
    {
        if (!isEmailUnique)
        {
            return Result.Failure<User>(
                UserErrors.EmailIsNotUnique(email.Value));
        }


        var user = new User(
            id,
            email,
            firstName,
            lastName,
            passwordHash,
            passwordSalt,
            emailConfirmationToken,
            isEmailConfirmed: false,
            role,
            gender,
            cart);

        //user.AddDomainEvent(
        //    new UserCreatedDomainEvent(Guid.NewGuid(), id));

        return user;
    }

    public static Result Login(User user, bool passwordIsCorrect)
    {
        if (!user.IsEmailConfirmed)
        {
            return Result.Failure(
                UserErrors.EmailIsNotConfirmed);
        }

        if (!passwordIsCorrect)
        {
            return Result.Failure(
                UserErrors.PasswordIsNotCorrect);
        }

        //user.AddDomainEvent(
        //    new UserLoggedInDomainEvent(Guid.NewGuid(), user.Id));

        return Result.Success();
    }


    public Result ConfirmEmail(EmailConfirmationToken refreshToken)
    {
        if (EmailConfirmationToken != refreshToken)
        {
            return Result.Failure(
                UserErrors.EmailConfirmationtokenIsnotCorrect);
        }

        IsEmailConfirmed = true;

        // TODO: add domain event
        //AddDomainEvent(
        //         new UserEmailConfirmedDomainEvent(Guid.NewGuid(), Id));

        return Result.Success();
    }

    public Result RetryEmailConfirmation(EmailConfirmationToken emailConfirmationToken)
    {
        if (IsEmailConfirmed)
        {
            return Result.Failure(
                UserErrors.EmailIsAlreadyConfirmed);
        }

        EmailConfirmationToken = emailConfirmationToken;

        return Result.Success();
    }

    public void ChangeEmail(Email email)
    {
        Email = email;
    }

    public Result AddInWishList(Wish wish)
    {
        if (_wishes.Any(w => w.Book == wish.Book))
        {
            return Result.Failure(
                UserErrors.BookIsAlreadyInWishList);
        }

        _wishes.Add(wish);

        return Result.Success();
    }

    public Result RemoveFromWishList(BookId bookId)
    {
        var wish = _wishes.FirstOrDefault(w => w.Book.Id == bookId);

        if (wish is null)
        {
            return Result.Failure(
                UserErrors.BookIsNotInWishList);
        }

        _wishes.Remove(wish);

        return Result.Success();
    }

    public void UpdateRefreshToken(RefreshToken refreshToken) =>
        RefreshToken = refreshToken;
}