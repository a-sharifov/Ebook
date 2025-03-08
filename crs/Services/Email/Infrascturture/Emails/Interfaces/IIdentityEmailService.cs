using Infrastructure.Emails.Models;

namespace Infrastructure.Emails.Interfaces;

public interface IIdentityEmailService : IEmailService
{
    Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, CancellationToken cancellationToken = default);
    Task SendForgotPasswordEmailAsync(SendForgotPasswordEmailRequest request, CancellationToken cancellationToken = default);
}
