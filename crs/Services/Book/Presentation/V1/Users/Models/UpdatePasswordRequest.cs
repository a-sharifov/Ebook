namespace Presentation.V1.Users.Controllers;

public sealed record UpdatePasswordRequest(
    [Required] string OldPassword,
    [Required] string NewPassword
    );
