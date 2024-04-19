using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;
using Infrastructure.Jwt.Interfaces;

namespace Application.Users.Commands.Login;

internal sealed class LoginCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IHashingService hashingService,
    IJwtManager jwtManager)
    : ICommandHandler<LoginCommand, LoginCommanResponse>
{
    private readonly IUserRepository _repository = repository; 
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHashingService _hashingService = hashingService;
    private readonly IJwtManager _jwtManager = jwtManager;

    public async Task<Result<LoginCommanResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginCommanResponse>(
                UserErrors.UserDoesNotExist);
        }

        var loginResult = Login(user, request.Password);

        if (loginResult.IsFailure)
        {
            return Result.Failure<LoginCommanResponse>(
                loginResult.Error);
        }

        var refreshToken = _jwtManager.CreateRefreshToken();

        user.UpdateRefreshToken(refreshToken);
        var userToken = _jwtManager.CreateTokenString(user, request.Audience);

        await _unitOfWork.Commit(cancellationToken);

        var response = new LoginCommanResponse(userToken, refreshToken.Token);

        return response;
    }

    private async Task<User?> GetUserByEmailAsync(string emailString, CancellationToken cancellationToken = default)
    {
        var emailResult = Email.Create(emailString);
        return await _repository.GetByEmailAsync(emailResult.Value, cancellationToken: cancellationToken);
    }

    private Result Login(User user, string password)
    {
        var passwordIsCorrect = _hashingService.Verify(
            password, user.PasswordSalt.Value, user.PasswordHash.Value);

        var loginResult = User.Login(user, passwordIsCorrect);

        return loginResult;
    }
}
