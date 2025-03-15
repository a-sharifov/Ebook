namespace Presentation.V1.Users.Models;

public record ConfirmChangePasswordWithoutOldRequest(
    [Required] Guid UserId,
    [Required] string ChangePasswordToken,
    [Required] string Password
    );
