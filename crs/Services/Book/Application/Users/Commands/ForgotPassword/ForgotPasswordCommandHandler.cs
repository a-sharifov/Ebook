using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;

namespace Application.Users.Commands.ForgotPassword;

internal sealed class ForgotPasswordCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<ForgotPasswordCommand>
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
        {
            return Result.Failure(emailResult.Error);
        }

        var email = emailResult.Value;
        var isExist = await _repository.IsExistAsync(email, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(
                UserErrors.UserDoesNotExist);
        }

        var user = await _repository.GetAsync(email, cancellationToken);

        var isConfirmed = user.IsEmailConfirmed;

        if (!isConfirmed)
        {
            return Result.Failure(
                UserErrors.EmailIsNotConfirmed);
        }

        var resetPasswordToken = ResetPasswordToken.Create();
        var setResetPasswordResult = user.SetResetPasswordToken(resetPasswordToken, request.ReturnUrl);

        if (setResetPasswordResult.IsFailure)
        {
            return Result.Failure(setResetPasswordResult.Error);
        }

        //await _identityEmailService.SendForgotPasswordEmailAsync(user, request.ReturnUrl, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        return Result.Success();
    }
}
