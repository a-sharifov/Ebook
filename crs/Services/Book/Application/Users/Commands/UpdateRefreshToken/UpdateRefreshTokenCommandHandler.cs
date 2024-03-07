using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Jwt.Interfaces;

namespace Application.Users.Commands.UpdateRefreshToken;

internal sealed class UpdateRefreshTokenCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IJwtManager jwtManager) :
    ICommandHandler<UpdateRefreshTokenCommand, UpdateRefreshTokenCommandResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IJwtManager _jwtManager = jwtManager;

    public async Task<Result<UpdateRefreshTokenCommandResponse>> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var emailString = _jwtManager.GetEmailFromToken(request.Token);
        var emailResult = Email.Create(emailString);

        var user = await _userRepository.GetUserByEmailAsync(emailResult.Value, cancellationToken);

        if (user is null ||
            user.RefreshToken?.Token != request.RefreshToken)
        {
            return Result.Failure<UpdateRefreshTokenCommandResponse>(
                UserErrors.RefreshTokenIsNotExists);
        }

        if(user.RefreshToken.IsExpired)
        {
            return Result.Failure<UpdateRefreshTokenCommandResponse>(
                UserErrors.RefreshTokenIsExpired);
        }

        var refreshToken = _jwtManager.CreateRefreshToken();
        var userToken = _jwtManager.CreateTokenString(user, request.Audience);

        user.UpdateRefreshToken(refreshToken);

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return new UpdateRefreshTokenCommandResponse(userToken, refreshToken.Token);
    }
}
