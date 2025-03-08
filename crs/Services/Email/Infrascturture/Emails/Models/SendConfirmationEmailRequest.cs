namespace Infrastructure.Emails.Models;

public sealed record SendConfirmationEmailRequest(
    string FirstName,
    string LastName,
    string UserId,
    string Email,
    string EmailConfirmationToken,
    string ReturnUrl
    );
