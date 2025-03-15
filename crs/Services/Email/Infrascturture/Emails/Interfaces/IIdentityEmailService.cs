using Infrastructure.Emails.Models;

namespace Infrastructure.Emails.Interfaces;

public interface IIdentityEmailService : IEmailService
{
    Task SendChangePasswordEmailAsync(SendChangePasswordEmailRequest emailRequest, CancellationToken cancellationToken);
    Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, CancellationToken cancellationToken = default);
    Task SendForgotPasswordEmailAsync(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken = default);
}
