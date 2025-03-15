namespace Presentation.V1.Users.Models;

public sealed record ChangePasswordWithoutOldRequest(
    [Required] Guid UserId,
    [Required] string ReturnUrl
    );
