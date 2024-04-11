namespace Presentation.V1.Users.Models;

public sealed record ConfirmEmailRequest(
    [Required] Guid UserId,
    [Required] string EmailConfirmationToken,
    [Required] string ReturnUrl);
