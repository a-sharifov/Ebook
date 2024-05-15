namespace Presentation.V1.Users.Controllers;

public sealed record ChangePasswordRequest(
    [Required] string Password
    );
