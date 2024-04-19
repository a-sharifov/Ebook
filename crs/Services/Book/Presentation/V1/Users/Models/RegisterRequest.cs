namespace Presentation.V1.Users.Models;

public sealed record RegisterRequest(
    [Required] string Email,
    [Required] string Password,
    [Required] string ConfirmPassword,
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string ReturnUrl);
