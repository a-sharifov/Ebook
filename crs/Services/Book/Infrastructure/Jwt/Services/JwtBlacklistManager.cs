using System.IdentityModel.Tokens.Jwt;
using Catalog.Persistence.Caching.Abstractions;
using Infrastructure.Jwt.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Jwt.Services;

public sealed class JwtBlacklistManager(
    ICachedService cachedService, 
    IJwtManager jwtManager) : IJwtBlacklistManager
{
    private readonly ICachedService _cachedService = cachedService;
    private readonly IJwtManager _jwtManager = jwtManager;

    public async Task RevokeAsync(string token, CancellationToken cancellationToken = default)
    {
        var jti = GetJtiFromToken(token);

        var tokenExpiry = GetExpiryFromToken(token).TimeOfDay;

        await _cachedService.SetAsync(token, jti, tokenExpiry, cancellationToken);
    }

    public async Task<bool> IsInListAsync(string token, CancellationToken cancellationToken = default) =>
         await _cachedService.GetAsync<string>(token, cancellationToken) != null;

    private string GetJtiFromToken(string token) =>
        _jwtManager.GetJtiFromToken(token);

    private static DateTime GetExpiryFromToken(string token)
    {
        var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        return jwtToken?.ValidTo ?? throw new InvalidOperationException("Expiry not found in JWT token.");
    }
}
 