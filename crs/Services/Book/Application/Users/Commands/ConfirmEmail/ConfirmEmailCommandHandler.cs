﻿using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;

namespace Application.Users.Commands.ConfirmEmail;

internal sealed class ConfirmEmailCommandHandler(
    IUserRepository repository, 
    IUnitOfWork unitofWork)
    : ICommandHandler<ConfirmEmailCommand>
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitofWork = unitofWork;

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var isExists = await _repository.IsExistAsync(userId, cancellationToken);

        if (!isExists)
        {
            return Result.Failure(
                UserErrors.UserDoesNotExist);
        }

        var user = await _repository.GetAsync(userId, cancellationToken: cancellationToken);

        if (user.IsEmailConfirmed)
        {
            return Result.Failure(
                UserErrors.UserIsConfirmEmail);
        }

        var requestEmailConfirmationTokenResult =
            EmailConfirmationToken.Create(request.EmailConfirmationToken);

        var confirmEmailResult = user.ConfirmEmail(
            requestEmailConfirmationTokenResult.Value);

        if (confirmEmailResult.IsFailure)
        {
            return Result.Failure(
                confirmEmailResult.Error);
        }

        await _repository.UpdateAsync(user, cancellationToken);
        await _unitofWork.Commit(cancellationToken);

        return Result.Success();
    }
}
