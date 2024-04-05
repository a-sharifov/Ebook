using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Email.Interfaces;
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
        var emailResult = Email.Create(request.Email);
        var user = await _repository
            .GetByEmailAsync(emailResult.Value, cancellationToken);

        if (user is null)
        {
            return Result.Failure(
                UserErrors.UserDoesNotExist);
        }

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
