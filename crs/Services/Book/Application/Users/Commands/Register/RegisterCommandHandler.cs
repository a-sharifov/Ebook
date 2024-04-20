using Contracts.Enumerations;
using Domain.SharedKernel.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;
using Infrastructure.Emails.Interfaces;
using Domain.CartAggregate;
using Domain.CartAggregate.Ids;
using Domain.WishAggregate.Ids;
using Domain.WishAggregate;

namespace Application.Users.Commands.Register;

internal sealed class RegisterCommandHandler(
    IHashingService hashingService,
    IUserRepository repository,
    IIdentityEmailService identityEmailService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterCommand>
{
    private readonly IHashingService _hashingService = hashingService;
    private readonly IUserRepository _repository = repository;
    private readonly IIdentityEmailService _identityEmailService = identityEmailService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userResult = await CreateUserAsync(request, cancellationToken);

        if (userResult.IsFailure)
        {
            return Result.Failure(userResult.Error);
        }

        var user = userResult.Value;

        await _repository.AddAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        await _identityEmailService.SendConfirmationEmailAsync(user, request.ReturnUrl, cancellationToken);

        return Result.Success();
    }

    private async Task<Result<User>> CreateUserAsync(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        var userId = new UserId(Guid.NewGuid());
        var email = Email.Create(request.Email).Value;
        var firstName = FirstName.Create(request.FirstName).Value;
        var lastName = LastName.Create(request.LastName).Value;

        var generateSalt = _hashingService.GenerateSalt();
        var passwordSalt = PasswordSalt.Create(generateSalt).Value;

        var hash = _hashingService.Hash(request.Password, generateSalt);
        var passwordHash = PasswordHash.Create(hash).Value;

        var emailConfirmationToken = _hashingService.GenerateToken();

        var role = Role.User;

        var isEmailUnique = await _repository
            .IsEmailUniqueAsync(email, cancellationToken);

        var cartId = new CartId(Guid.NewGuid());
        var cart = Cart.Create(cartId, userId).Value;

        var wishId = new WishId(Guid.NewGuid());
        var wish = Wish.Create(wishId, userId).Value;

        var user = User.Create(
            userId,
            email,
            firstName,
            lastName,
            passwordHash,
            passwordSalt,
            emailConfirmationToken,
            isEmailUnique,
            role,
            cart,
            wish);

        return user;
    }
}
