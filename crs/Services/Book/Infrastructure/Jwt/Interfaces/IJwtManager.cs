using Domain.UserAggregate.ValueObjects;
using Domain.UserAggregate;
using System.Security.Claims;

namespace Infrastructure.Jwt.Interfaces;

public interface IJwtManager
{
    int TokenExpirationTimeMinutes { get; }
    int RefreshTokenExpirationTimeMinutes { get; }

    string CreateTokenString(User user);
    string UpdateTokenString(string token);
    string CreateRefreshTokenString();
    RefreshToken CreateRefreshToken();
    IEnumerable<Claim> GetClaimsInToken(string token);
    string GetEmailFromToken(string oldToken);
    string GetJtiFromToken(string token);
}
