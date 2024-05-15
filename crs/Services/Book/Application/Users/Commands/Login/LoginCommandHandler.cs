using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Hashing.Interfaces;
using Infrastructure.Jwt.Interfaces;
using Persistence;

namespace Application.Users.Commands.Login;

internal sealed class LoginCommandHandler(
    IUserRepository repository,
    IHashingService hashingService,
    IJwtManager jwtManager,
    IUnitOfWork unitOfWork)
    : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    private readonly IUserRepository _repository = repository; 
    private readonly IHashingService _hashingService = hashingService;
    private readonly IJwtManager _jwtManager = jwtManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email;

        var isEmailExist = await IsEmailExistAsync(email, cancellationToken);

        if (!isEmailExist)
        {
            return Result.Failure<LoginCommandResponse>(
                UserErrors.EmailIsNotExists(email));
        }

        var user = await GetUserByEmailAsync(email, cancellationToken);

        var loginResult = Login(user, request.Password);

        if (loginResult.IsFailure)
        {
            return Result.Failure<LoginCommandResponse>(
                loginResult.Error);
        }

        var refreshToken = _jwtManager.CreateRefreshToken();

        user.UpdateRefreshToken(refreshToken);
        var userToken = _jwtManager.CreateTokenString(user);

        await _repository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        var response = new LoginCommandResponse(userToken, refreshToken.Token);

        return response;
    }

    private async Task<bool> IsEmailExistAsync(string userEmail, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(userEmail).Value;

        var isEmailExist = await _repository.IsExistAsync(
            email, cancellationToken);

        return isEmailExist;
    }

    private async Task<User> GetUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default)
    {
        var email = Email.Create(userEmail).Value;
        var user = await _repository.GetAsync(email, cancellationToken: cancellationToken);
        return user;
    }

    private Result Login(User user, string password)
    {
        var passwordIsCorrect = _hashingService.Verify(
            password, user.PasswordSalt, user.PasswordHash);

        var loginResult = user.Login(passwordIsCorrect);

        return loginResult;
    }
}
