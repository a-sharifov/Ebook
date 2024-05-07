using Domain.UserAggregate.Errors;
using Infrastructure.Jwt.Interfaces;

namespace Application.Users.Commands.Logout;

internal sealed class LogoutCommandHandler(
    IJwtBlacklistManager blacklistManager) 
    : ICommandHandler<LogoutCommand>
{
    private readonly IJwtBlacklistManager _blacklistManager = blacklistManager;

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var isInList = await _blacklistManager.IsInListAsync(request.Token, cancellationToken);

        if (isInList)
        {
            return Result.Failure(
                JwtErrors.JwtInBlackList);
        }

        await _blacklistManager.RevokeAsync(request.Token, cancellationToken);
        return Result.Success();
    }
}
