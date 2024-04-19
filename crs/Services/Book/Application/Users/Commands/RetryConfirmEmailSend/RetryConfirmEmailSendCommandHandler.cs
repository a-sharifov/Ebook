using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Emails.Interfaces;
using Infrastructure.Hashing.Interfaces;

namespace Application.Users.Commands.RetryConfirmEmailSend;

public class RetryConfirmEmailSendCommandHandler(
    IUserRepository repository,
    IHashingService hashingService,
    IIdentityEmailService identityEmailService)
    : ICommandHandler<RetryConfirmEmailSendCommand>
{
    private readonly IUserRepository _repository = repository;
    private readonly IHashingService _hashingService = hashingService;
    private readonly IIdentityEmailService _identityEmailService = identityEmailService;

    public async Task<Result> Handle(RetryConfirmEmailSendCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email).Value;
        var isEmailExists = await _repository.IsEmailExistAsync(email, cancellationToken);

        if (!isEmailExists)
        {
            return Result.Failure(
                UserErrors.EmailIsNotExists(email.Value));
        }

        var user = await _repository
            .GetByEmailAsync(email, cancellationToken: cancellationToken);

        var confirmationEmailToken = _hashingService.GenerateToken();
        var RetryEmailConfirmationResult =
            user.RetryEmailConfirmation(confirmationEmailToken);

        if (RetryEmailConfirmationResult.IsFailure)
        {
            return Result.Failure(
                RetryEmailConfirmationResult.Error);
        }

        await _identityEmailService.SendConfirmationEmailAsync(user, request.ReturnUrl, cancellationToken);

        return Result.Success();
    }
}
