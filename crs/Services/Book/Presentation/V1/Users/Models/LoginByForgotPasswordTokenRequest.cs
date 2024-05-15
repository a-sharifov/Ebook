namespace Presentation.V1.Users.Models;

public sealed record LoginByForgotPasswordTokenRequest(
    [Required] Guid UserId,
    [Required] string ResetPasswordToken
    );
