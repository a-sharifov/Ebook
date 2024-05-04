
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;

namespace Application.Users.Commands.UpdatePassword;

internal sealed class UpdatePasswordCommandHadnler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IHashingService hashingService)
    : ICommandHandler<UpdatePasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashingService _hashingService = hashingService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var id = new UserId(request.UserId);
        var user = await _userRepository.GetAsync(id, cancellationToken);

        var updatePasswordResult = UpdatePassword(user, request.OldPassword, request.NewPassword);

        if (updatePasswordResult.IsFailure)
        {
            return updatePasswordResult;
        }

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }

    private Result UpdatePassword(User user, string oldPassword, string newPassword)
    {
        var passwordIsCorrect = _hashingService.Verify(
            oldPassword, user.PasswordSalt.Value, user.PasswordHash.Value);

        var generateSalt = _hashingService.GenerateSalt();
        var passwordSalt = PasswordSalt.Create(generateSalt).Value;

        var hash = _hashingService.Hash(newPassword, generateSalt);
        var passwordHash = PasswordHash.Create(hash).Value;

        var updatePasswordResult = user.UpdatePassword(passwordHash, passwordSalt, passwordIsCorrect);

        return updatePasswordResult;
    }
}