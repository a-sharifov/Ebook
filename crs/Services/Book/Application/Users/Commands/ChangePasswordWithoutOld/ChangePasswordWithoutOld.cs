using Application.Users.Commands.ChangePassword;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;

namespace Application.Users.Commands.ChangePasswordWithoutOld;

internal sealed class ChangePasswordWithoutOldCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<ChangePasswordWithoutOldCommand>
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangePasswordWithoutOldCommand request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);


        var isExist = await _repository.IsExistAsync(userId, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                UserErrors.UserDoesNotExist);
        }

        var user = await _repository.GetAsync(userId, cancellationToken);

        var changePasswordToken = ChangePasswordToken.Create();
        var setResetPasswordResult = user.SetChangePasswordToken(changePasswordToken, request.ReturnUrl);

        if (setResetPasswordResult.IsFailure)
        {
            return Result.Failure(setResetPasswordResult.Error);
        }

        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }
}

