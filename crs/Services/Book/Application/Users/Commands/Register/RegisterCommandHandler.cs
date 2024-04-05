using Contracts.Enumerations;
using Domain.SharedKernel.ValueObjects;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;
using Infrastructure.Email.Interfaces;
using Domain.CartAggregate;
using Domain.CartAggregate.Ids;

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
        var userResult = await CreateUserResultAsync(request, cancellationToken);

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

    private async Task<Result<User>> CreateUserResultAsync(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        var userId = new UserId(Guid.NewGuid());
        var emailResult = Email.Create(request.Email);
        var firstNameResult = FirstName.Create(request.FirstName);
        var lastNameResult = LastName.Create(request.LastName);

        var generateSalt = _hashingService.GenerateSalt();
        var passwordSaltResult = PasswordSalt.Create(generateSalt);

        var hash = _hashingService.Hash(request.Password, generateSalt);
        var passwordHashResult = PasswordHash.Create(hash);

        var emailConfirmationToken = _hashingService.GenerateToken();

        var role = Role.FromName(request.Role);
        var gender = Gender.FromName(request.Gender);

        var isEmailUnique = await _repository
            .IsEmailUnigueAsync(emailResult.Value, cancellationToken);

        var cartId = new CartId(Guid.NewGuid());
        var cart = Cart.Create(cartId, userId);

        var user = User.Create(
            userId,
            emailResult.Value,
            firstNameResult.Value,
            lastNameResult.Value,
            passwordHashResult.Value,
            passwordSaltResult.Value,
            emailConfirmationToken,
            isEmailUnique,
            role,
            gender,
            cart.Value);

        return user;
    }
}
