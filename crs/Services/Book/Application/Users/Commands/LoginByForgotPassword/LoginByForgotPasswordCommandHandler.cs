using Application.Users.Commands.Login;
using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Ids;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Jwt.Interfaces;

namespace Application.Users.Commands.LoginByForgotPassword;

internal sealed class LoginByForgotPasswordCommandHandler(
    IJwtManager jwtManager,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<LoginByForgotPasswordCommand, LoginCommandResponse>
{
    private readonly IJwtManager _jwtManager = jwtManager;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<LoginCommandResponse>> Handle(LoginByForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var id = new UserId(request.UserId);
        var isExist = await _userRepository.IsExistAsync(id, cancellationToken);

        if (!isExist)
        {
            return Result.Failure<LoginCommandResponse>(UserErrors.UserDoesNotExist);
        }

        var user = await _userRepository.GetAsync(id, cancellationToken);

        var resetPasswordTokenResult = ResetPasswordToken.Create(request.ForgotPasswordToken);

        if (resetPasswordTokenResult.IsFailure)
        {
            return Result.Failure<LoginCommandResponse>(resetPasswordTokenResult.Error);
        }

        var resetPasswordToken = resetPasswordTokenResult.Value;

        var loginResult = user.Login(resetPasswordToken);

        if(loginResult.IsFailure)
        {
            return Result.Failure<LoginCommandResponse>(loginResult.Error);
        }

        var refreshToken = _jwtManager.CreateRefreshToken();

        user.UpdateRefreshToken(refreshToken);
        var userToken = _jwtManager.CreateTokenString(user);

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        var response = new LoginCommandResponse(userToken, refreshToken.Token);

        return response;
    }

}
    
