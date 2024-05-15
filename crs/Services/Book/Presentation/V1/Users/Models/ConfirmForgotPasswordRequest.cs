namespace Presentation.V1.Users.Models;

public record ConfirmForgotPasswordRequest(
    [Required] Guid UserId,
    [Required] string ResetPasswordToken,
    [Required] string ReturnUrl
    );
