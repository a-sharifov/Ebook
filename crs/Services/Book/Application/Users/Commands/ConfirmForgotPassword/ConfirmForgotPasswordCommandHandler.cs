﻿using Application.Users.Commands.ForgotPassword;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;

namespace Application.Users.Commands.ConfirmForgotPassword;

internal sealed class ConfirmForgotPasswordCommandHandler(IUserRepository userRepository)
    : ICommandHandler<ConfirmForgotPasswordCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> Handle(ConfirmForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var resetPasswordTokenResult = ResetPasswordToken.Create(request.ResetPasswordToken);

        if (resetPasswordTokenResult.IsFailure)
        {
            return Result.Failure(resetPasswordTokenResult.Error);
        }

        var resetPasswordToken = resetPasswordTokenResult.Value;

        var id = new UserId(request.Id);
        var isExist = await _userRepository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure(UserErrors.UserDoesNotExist);
        }

        var user = await _userRepository.GetAsync(id, cancellationToken);
        var confirmForgotPasswordResult = user.ConfirmForgotPassword(resetPasswordToken);

        if (confirmForgotPasswordResult.IsFailure)
        {
            return Result.Failure(confirmForgotPasswordResult.Error);
        }

        return Result.Success();
    }
}
