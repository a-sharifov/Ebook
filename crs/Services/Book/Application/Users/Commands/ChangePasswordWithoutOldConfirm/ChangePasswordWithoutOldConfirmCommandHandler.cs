using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;

namespace Application.Users.Commands.ChangePasswordWithoutOldConfirm;

internal sealed class ChangePasswordWithoutOldConfirmCommandHandler(
    IUserRepository userRepository, 
    IUnitOfWork unitOfWork, 
    IHashingService hashingService)
    : ICommandHandler<ChangePasswordWithoutOldConfirmCommand>
{
    private readonly IHashingService _hashingService = hashingService;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangePasswordWithoutOldConfirmCommand request, CancellationToken cancellationToken)
    {
        var changePasswordTokenResult = ChangePasswordToken.Create(request.ResetPasswordToken);

        if (changePasswordTokenResult.IsFailure)
        {
            return Result.Failure(changePasswordTokenResult.Error);
        }

        var changePasswordToken = changePasswordTokenResult.Value;

        var id = new UserId(request.Id);
        var isExist = await _userRepository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(UserErrors.UserDoesNotExist);
        }

        var user = await _userRepository.GetAsync(id, cancellationToken);

        var generateSalt = _hashingService.GenerateSalt();
        var passwordSaltResult = PasswordSalt.Create(generateSalt);

        var hash = _hashingService.Hash(request.Password, generateSalt);
        var passwordHashResult = PasswordHash.Create(hash);

        var changePasswordResult = user.ChangePassword(
            changePasswordToken, passwordHashResult.Value, passwordSaltResult.Value);

        if (changePasswordResult.IsFailure)
        {
            return Result.Failure(changePasswordResult.Error);
        }

        await _unitOfWork.Commit();

        return Result.Success();
    }
}
