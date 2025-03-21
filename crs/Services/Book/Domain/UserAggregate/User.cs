﻿using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.ValueObjects;
using Domain.CartAggregate;
using Domain.WishAggregate;
using Domain.UserAggregate.DomainEvents;
using Contracts.Enumerations;

namespace Domain.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public PasswordSalt PasswordSalt { get; private set; }
    public RefreshToken? RefreshToken { get; private set; }
    public EmailConfirmationToken? EmailConfirmationToken { get; private set; }
    public ResetPasswordToken? ResetPasswordToken { get; private set; }
    public ChangePasswordToken? ChangePasswordToken { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public Role Role { get; private set; }
    public Cart Cart { get; private set; }
    public Wish Wish { get; private set; }
    //todo: Make Aggregate Root

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
      bool isEmailConfirmed,
      Role role,
      Cart cart,
      Wish wish)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        IsEmailConfirmed = isEmailConfirmed;
        Role = role;
        Cart = cart;
        Wish = wish;
    }

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
        Cart cart,
        Wish wish) 
        : this(id, email, firstName, lastName, passwordHash, passwordSalt, isEmailConfirmed, role, cart, wish)
    {
        EmailConfirmationToken = emailConfirmationToken;
    }

    public static Result<User> Create(
        UserId id,
        Email email,
        FirstName firstName,
        LastName lastName,
        PasswordHash passwordHash,
        PasswordSalt passwordSalt,
        bool isEmailUnique,
        EmailConfirmationToken emailConfirmationToken,
        Role role,
        Cart cart,
        Wish wish, 
        string returnUrl)
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
            cart,
            wish);

        user.AddDomainEvent(
            new UserConfirmCreatedDomainEvent(
                Guid.NewGuid(), id, returnUrl));

        return user;
    }

    public static Result<User> Create(
      UserId id,
      Email email,
      FirstName firstName,
      LastName lastName,
      PasswordHash passwordHash,
      PasswordSalt passwordSalt,
      Role role,
      Cart cart,
      Wish wish)
    {
        var user = new User(
            id,
            email,
            firstName,
            lastName,
            passwordHash,
            passwordSalt,
            isEmailConfirmed: true,
            role,
            cart,
            wish);

        return user;
    }

    public Result Login(bool passwordIsCorrect)
    {
        if (!IsEmailConfirmed)
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


    public Result Login(ResetPasswordToken resetPasswordToken)
    {
        if(ResetPasswordToken == null)
        {
            return Result.Failure(
                UserErrors.ResetPasswordTokenIsNotSet);
        }

        if (ResetPasswordToken != resetPasswordToken)
        {
            return Result.Failure(
                UserErrors.ResetPasswordTokenIsNotCorrect);
        }

        //user.AddDomainEvent(
        //    new UserLoggedInDomainEvent(Guid.NewGuid(), user.Id));

        ResetPasswordToken = null;
        return Result.Success();
    }



    public Result ConfirmEmail(EmailConfirmationToken confirmationToken)
    {
        if (EmailConfirmationToken != confirmationToken)
        {
            return Result.Failure(
                UserErrors.EmailConfirmationtokenIsnotCorrect);
        }

        IsEmailConfirmed = true;
        EmailConfirmationToken = null;

        // TODO: add domain event
        //AddDomainEvent(
        //         new UserEmailConfirmedDomainEvent(Guid.NewGuid(), Id));

        return Result.Success();
    }

    public Result RetryEmailConfirmation(
        EmailConfirmationToken emailConfirmationToken, string returnUrl)
    {
        if (IsEmailConfirmed)
        {
            return Result.Failure(
                UserErrors.EmailIsAlreadyConfirmed);
        }

        EmailConfirmationToken = emailConfirmationToken;

        AddDomainEvent(
            new UserRetryEmailConfirmationDomainEvent(
                Guid.NewGuid(), Id, returnUrl));

        return Result.Success();
    }

    public Result ChangeEmail(Email email)
    {
        if(Email == email)
        {
            return Result.Failure(
                UserErrors.IsCurrentEmail);
        }

        Email = email;
        return Result.Success();
    }

    public void UpdateRefreshToken(RefreshToken refreshToken) =>
        RefreshToken = refreshToken;

    public Result UpdatePassword(PasswordHash passwordHash, PasswordSalt passwordSalt)
    {
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;

        return Result.Success();
    }

    public Result SetResetPasswordToken(ResetPasswordToken resetPasswordToken, string returnUrl)
    {
        ResetPasswordToken = resetPasswordToken;
        AddDomainEvent(
            new UserResetPasswordTokenDomainEvent(
                Guid.NewGuid(),Id, returnUrl));

        return Result.Success();
    }

    public Result SetChangePasswordToken(ChangePasswordToken changePasswordToken, string returnUrl)
    {
        ChangePasswordToken = changePasswordToken;
        AddDomainEvent(
            new UserChangePasswordTokenDomainEvent(
                Guid.NewGuid(), Id, returnUrl));

        return Result.Success();
    }

    public Result ChangePassword(ChangePasswordToken changePasswordToken, PasswordHash passwordHash, PasswordSalt passwordSalt)
    {
        if (ChangePasswordToken == null)
        {
            return Result.Failure(
                UserErrors.ResetPasswordTokenIsNotSet);
        }

        if (ChangePasswordToken != changePasswordToken)
        {
            return Result.Failure(
                UserErrors.ResetPasswordTokenIsNotCorrect);
        }

        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;

        return Result.Success();
    }

    public Result ConfirmForgotPassword(ResetPasswordToken resetPasswordToken)
    {
        if(ResetPasswordToken == null)
        {
            return Result.Failure(
                UserErrors.ResetPasswordTokenIsNotSet);
        }

        if(ResetPasswordToken != resetPasswordToken)
        {
            return Result.Failure(
                UserErrors.ResetPasswordTokenIsNotCorrect);
        }

        return Result.Success();
    }
}