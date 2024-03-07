using Infrastructure.Email.Models;

namespace Infrastructure.Email.Abstractions;

public interface IIdentityEmailService : IEmailService
{
    Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, CancellationToken cancellationToken = default);
}
