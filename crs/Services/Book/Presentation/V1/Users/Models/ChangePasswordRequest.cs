namespace Presentation.V1.Users.Controllers;

public sealed record ChangePasswordRequest(
    [Required] string OldPassword,
    [Required] string NewPassword
    );
