namespace Infrastructure.Emails.Models;

public sealed record SendForgotPasswordEmailRequest(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string ResetPasswordToken,
    string ReturnUrl
    );
