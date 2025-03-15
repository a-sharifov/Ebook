namespace Infrastructure.Emails.Models;

public sealed record SendChangePasswordEmailRequest(
    string FirstName,
    string LastName,
    string UserId,
    string Email,
    string ResetPasswordToken,
    string ReturnUrl
    );
