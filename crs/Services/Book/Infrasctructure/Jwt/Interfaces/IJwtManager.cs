using Domain.UserAggregate.ValueObjects;
using Domain.UserAggregate;
using System.Security.Claims;

namespace Infrasctructure.Jwt.Interfaces;

public interface IJwtManager
{
    int TokenExpirationTimeMinutes { get; }
    int RefreshTokenExpirationTimeMinutes { get; }

    string CreateTokenString(User user, string audience);
    string CreateRefreshTokenString();
    RefreshToken CreateRefreshToken();
    IEnumerable<Claim> GetClaimsInToken(string token);
    string GetEmailFromToken(string token);
}
