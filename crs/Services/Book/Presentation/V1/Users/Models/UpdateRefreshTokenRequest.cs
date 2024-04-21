namespace Presentation.V1.Users.Models;

public sealed record UpdateRefreshTokenRequest(
    [Required] string Token,
    [Required] string RefreshToken);
