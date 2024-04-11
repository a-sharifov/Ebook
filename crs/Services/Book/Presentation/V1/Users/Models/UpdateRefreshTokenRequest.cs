namespace Presentation.V1.Users.Models;

public record class UpdateRefreshTokenRequest(
    [Required] string Token,
    [Required] string RefreshToken,
    [Required] string Audience);
