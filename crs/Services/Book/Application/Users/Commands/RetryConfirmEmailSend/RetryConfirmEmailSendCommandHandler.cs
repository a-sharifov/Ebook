using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;

namespace Application.Users.Commands.RetryConfirmEmailSend;

public class RetryConfirmEmailSendCommandHandler(
    IUserRepository userRepository,
    IHashingService hashingService)
    : ICommandHandler<RetryConfirmEmailSendCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashingService _hashingService = hashingService;

    public async Task<Result> Handle(RetryConfirmEmailSendCommand request, CancellationToken cancellationToken)
    {
        var emailResult = Email.Create(request.Email);
        var user = await _userRepository
            .GetUserByEmailAsync(emailResult.Value, cancellationToken);

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

        return Result.Success();
    }
}
