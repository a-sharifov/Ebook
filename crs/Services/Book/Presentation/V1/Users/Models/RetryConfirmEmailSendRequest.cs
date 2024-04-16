namespace Presentation.V1.Users.Models;

public sealed record RetryConfirmEmailSendRequest([Required] string Email, string ReturnUrl);
