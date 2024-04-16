namespace Presentation.V1.Users.Models;

public sealed record LoginRequest(
    [Required] string Email,
    [Required] string Password);
