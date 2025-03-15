using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;

namespace Application.Users.Commands.RetryConfirmEmailSend;

public class RetryConfirmEmailSendCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RetryConfirmEmailSendCommand>
{
    private readonly IUserRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(RetryConfirmEmailSendCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);

        if (emailResult.IsFailure)
        {
            return emailResult;
        }

        var email = emailResult.Value;

        var isEmailExists = await _repository.IsExistAsync(email, cancellationToken);

        if (!isEmailExists)
        {
            return Result.Failure(
                UserErrors.EmailIsNotExists(email.Value));
        }

        var user = await _repository
            .GetAsync(email, cancellationToken: cancellationToken);

        var emailConfirmationToken = EmailConfirmationToken.Create();

        var RetryEmailConfirmationResult =
            user.RetryEmailConfirmation(emailConfirmationToken, request.ReturnUrl);

        if (RetryEmailConfirmationResult.IsFailure)
        {
            return Result.Failure(
                RetryEmailConfirmationResult.Error);
        }

        await _repository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return Result.Success();
    }

}
