using Domain.UserAggregate;
using Infrastructure.Email.Models;

namespace Infrastructure.Email.Interfaces;

public interface IIdentityEmailService : IEmailService
{
    Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, CancellationToken cancellationToken = default);
    Task SendConfirmationEmailAsync(User user, string returnUrl, CancellationToken cancellationToken = default);
}
