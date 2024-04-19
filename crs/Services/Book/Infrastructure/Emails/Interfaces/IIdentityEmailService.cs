using Domain.UserAggregate;
using Infrastructure.Emails.Models;

namespace Infrastructure.Emails.Interfaces;

public interface IIdentityEmailService : IEmailService
{
    Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, CancellationToken cancellationToken = default);
    Task SendConfirmationEmailAsync(User user, string returnUrl, CancellationToken cancellationToken = default);
}
