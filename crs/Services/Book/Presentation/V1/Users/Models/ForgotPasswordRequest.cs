namespace Presentation.V1.Users.Models;

public sealed record ForgotPasswordRequest(
    [Required] string Email,
    [Required] string ReturnUrl
    );
