
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;

namespace Application.Users.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHadnler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IHashingService hashingService)
    : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashingService _hashingService = hashingService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var id = new UserId(request.UserId);
        var user = await _userRepository.GetAsync(id, cancellationToken);

        var updatePasswordResult = UpdatePassword(user, request.Password);

        if (updatePasswordResult.IsFailure)
        {
            return updatePasswordResult;
        }

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }

    private Result UpdatePassword(User user, string password)
    {
        var generateSalt = _hashingService.GenerateSalt();
        var passwordSaltResult = PasswordSalt.Create(generateSalt);

        var hash = _hashingService.Hash(password, generateSalt);
        var passwordHashResult = PasswordHash.Create(hash);

        var firstFailureOrSuccessResult = Result.FirstFailureOrSuccess(passwordSaltResult, passwordHashResult);

        if (firstFailureOrSuccessResult.IsFailure)
        {
            return firstFailureOrSuccessResult;
        }

        var updatePasswordResult = user.UpdatePassword(
            passwordHashResult.Value, passwordSaltResult.Value);

        return updatePasswordResult;
    }
}