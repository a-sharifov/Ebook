using Infrastructure.Email.Models;

namespace Infrastructure.Email.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Send email async.
    /// </summary>
    /// <param name="request">The <see cref="SendMessageRequest"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns></returns>
    Task SendMessageAsync(SendMessageRequest request, CancellationToken cancellationToken = default);
}