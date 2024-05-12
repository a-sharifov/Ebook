using Domain.Core.UnitOfWorks.Interfaces;
using Domain.UserAggregate.Errors;
using Domain.UserAggregate.Repositories;
using Domain.UserAggregate.ValueObjects;
using Infrastructure.Jwt.Interfaces;

namespace Application.Users.Commands.UpdateRefreshToken;

internal sealed class UpdateRefreshTokenCommandHandler(
    IUserRepository repository,
    IJwtManager jwtManager,
    IUnitOfWork unitOfWork) :
    ICommandHandler<UpdateRefreshTokenCommand, UpdateRefreshTokenCommandResponse>
{
    private readonly IUserRepository _repository = repository;
    private readonly IJwtManager _jwtManager = jwtManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UpdateRefreshTokenCommandResponse>> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var emailString = _jwtManager.GetEmailFromToken(request.Token);
        var emailResult = Email.Create(emailString);

        if (emailResult.IsFailure)
        {
            return Result.Failure<UpdateRefreshTokenCommandResponse>(
                emailResult.Error);
        }

        var email = emailResult.Value;

        var user = await _repository.GetAsync(email, cancellationToken: cancellationToken);

        if (user.RefreshToken?.Token != request.RefreshToken)
        {
            return Result.Failure<UpdateRefreshTokenCommandResponse>(
                UserErrors.RefreshTokenIsNotCorrect);
        }

        if (user.RefreshToken.IsExpired)
        {
            return Result.Failure<UpdateRefreshTokenCommandResponse>(
                UserErrors.RefreshTokenIsExpired);
        }

        var refreshToken = _jwtManager.CreateRefreshToken();
        var userToken = _jwtManager.CreateTokenString(user);

        user.UpdateRefreshToken(refreshToken);

        await _repository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        var response = new UpdateRefreshTokenCommandResponse(userToken, refreshToken.Token);

        return response;
    }
}
